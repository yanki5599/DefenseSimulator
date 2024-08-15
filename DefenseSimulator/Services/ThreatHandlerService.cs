using DefenseSimulator.Data;
using System.Collections.Concurrent;
using DefenseSimulator.Models;
using DefenseSimulator.Enums;
using DefenseSimulator.Hubs;
using System.Diagnostics;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Data;

namespace DefenseSimulator.Services
{
    public class ThreatHandlerService
    {
        private static ConcurrentDictionary<int, CancellationTokenSource> _Threats = new ConcurrentDictionary<int, CancellationTokenSource>();
        private static ConcurrentDictionary<int, CancellationTokenSource> _Responses = new ConcurrentDictionary<int, CancellationTokenSource>();
        private readonly DefenseSimulatorContext _context;
        private readonly IHubContext<NotificationHub> _hubContext;
        public ThreatHandlerService(DefenseSimulatorContext ctx , IHubContext<NotificationHub> notificationHub)
        {
            _context = ctx;
        }

        //create an Threat from Threat model
        public async Task CreateThreat(Threat threat)
        {
            _context.Threat.Add(threat);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> RegisterAndRunThreatTask(int ThreatId)
        {
            Threat? threat = _context.Threat.Find(ThreatId);
            bool isRunning = IsThreatRunning(ThreatId);


            threat.LaunchTime = DateTime.Now;
            threat.ThreatStatus = ThreatStatus.Active;
            await _context.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            _Threats[threat.ThreatId] =  cts;

            _ = Task.Run(() => RunTask(threat.ThreatId, cts.Token), cts.Token);

            return true;
        }

        // a method to check if Threat is running
        public bool IsThreatRunning(int ThreatId)
        {
            Threat? Threat = _context.Threat.Find(ThreatId);
            if (Threat == null || Threat.ThreatStatus != ThreatStatus.Active) return false;

            return _Threats.ContainsKey(Threat.ThreatId);
        }

        public async Task<bool> RemoveThreat(int ThreatId , ThreatStatus updatedStatus)
        {
            Threat? Threat = _context.Threat.Find(ThreatId);

            bool isRunning = IsThreatRunning(ThreatId);

            if (isRunning == false)
            {
                return false;
            }

            _Threats.TryRemove(Threat.ThreatId, out CancellationTokenSource? cts);
            cts?.Cancel();


            Threat.ThreatStatus = updatedStatus;
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task RunTask(int ThreatId, CancellationToken token)
        {
            try
            {
                var currThreat = await _context.Threat.FindAsync(ThreatId);
                
                while (currThreat.KaBoomTime < DateTime.UtcNow && !token.IsCancellationRequested)
                {
                    await Task.Delay(1000, token); 
                   
                    var message = $"Threat {ThreatId} exploding in  {currThreat.KaBoomTime} seconds.";
                    Console.WriteLine(message);
                    await _hubContext.Clients.All.SendAsync("ReceiveProgress", message);
                }

                // Finished
                if (!token.IsCancellationRequested)
                {
                    await _hubContext.Clients.All.SendAsync("ReceiveProgress", $"Threat {ThreatId} Exploded.");
                    RemoveThreat(ThreatId, ThreatStatus.Exploded);
                }
            }
            catch (TaskCanceledException)
            {
                await _hubContext.Clients.All.SendAsync("ReceiveProgress", $"Threat {ThreatId} aborted.");
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine(ex.ToString() + "reference threat is null");
            }
            
        }

        internal void RebuildActiveThreats()
        {
            if (_Threats.Count > 0)
            {
                return;
            }

            var Threats = _context.Threat.Where(a => a.ThreatStatus == ThreatStatus.Active).ToList();
            foreach (var Threat in Threats)
            {
                
                var cts = new CancellationTokenSource();
                _Threats[Threat.ThreatId] = cts;
                _ = Task.Run(() => RunTask(Threat.ThreatId, cts.Token), cts.Token);
                
            }
        }

        internal async Task<bool> InterseptThreat(int arsenalId)
        {
            var threatToIntersept = GetThreatToIntersept();
            if( threatToIntersept == null) return false;

            var currArsenal = await _context.Arsenal
                .Include(a => a.DefenseWeapon)
                .FirstAsync(a => a.Id == arsenalId);

            if (currArsenal == null) throw new ArgumentException("arsenal is null");


            Response response = new Response() 
            { 
                ThreatId = threatToIntersept.ThreatId,
                DefenseWeaponId = currArsenal.DefenseWeaponId,
                status = ResponseStatus.Active
            };

            await _context.Response.AddAsync(response);


            var cts = new CancellationTokenSource();
            _Responses[response.ResponseId] = cts;

            _ = Task.Run(() => RunTaskResponse(response, cts.Token));

            return true;
        }

        internal async Task RunTaskResponse(Response response, CancellationToken token)
        {
            try
            {
                while(response.KaboomboomTime < DateTime.UtcNow && !token.IsCancellationRequested)
                {
                    await Task.Delay(1000, token);
                    var message = $"Response {response.ResponseId} Intersepting Threat {response.ThreatId} in  {response.KaboomboomTime} seconds.";
                    Console.WriteLine(message);
                    await _hubContext.Clients.All.SendAsync("ReceiveProgress", message);

                }

                //finished
                await RemoveThreat(response.ThreatId,ThreatStatus.Intersepted);
                await RemoveResponse(response.ResponseId,ResponseStatus.Success);

            }catch(TaskCanceledException ex)
            {
                await RemoveResponse(response.ResponseId, ResponseStatus.Fail);
            }
        }

        private async Task RemoveResponse(int responseId , ResponseStatus updatedStatus)
        {
            Response? response = _context.Response.Find(responseId);

            

            _Threats.TryRemove(responseId, out CancellationTokenSource? cts);
            cts?.Cancel();


            response.status = updatedStatus;
            await _context.SaveChangesAsync();

        }

        private Threat? GetThreatToIntersept()
        {
            return _context.Threat.Where(t=>t.ThreatStatus == ThreatStatus.Active).MinBy(t=>t.KaBoomTime);
        }
    }
}
