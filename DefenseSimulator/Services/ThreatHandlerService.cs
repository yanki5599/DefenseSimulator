using DefenseSimulator.Data;
using System.Collections.Concurrent;
using DefenseSimulator.Models;
using DefenseSimulator.Hubs;

namespace DefenseSimulator.Services
{
    public class ThreatHandlerService
    {
        private static ConcurrentDictionary<string, CancellationTokenSource> _Threats = new ConcurrentDictionary<string, CancellationTokenSource>();
        private readonly DefenseSimulatorContext _context;
        private readonly NotificationHub notificationHub;
        public ThreatHandlerService(DefenseSimulatorContext ctx , NotificationHub notificationHub)
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
            Threat? Threat = _context.Threat.Find(ThreatId);
            bool isRunning = IsThreatRunning(ThreatId);

            if (Threat == null || Threat.IsInterceptedOrExploded || isRunning) return false;

            var ThreatActiveId = Guid.NewGuid().ToString();
            Threat.LaunchTime = DateTime.Now;
            Threat.ActiveID = ThreatActiveId;
            Threat.IsActive = true;
            await _context.SaveChangesAsync();

            var cts = new CancellationTokenSource();
            _Threats[ThreatActiveId] = cts;

            /*green non awaited warning without _ = */
            _ = Task.Run(() => RunTask(ThreatActiveId, cts.Token), cts.Token);

            return true;
        }

        // a method to check if Threat is running
        public bool IsThreatRunning(int ThreatId)
        {
            Threat? Threat = _context.Threat.Find(ThreatId);
            if (Threat == null ||
                Threat.IsActive == false ||
                Threat.ActiveID == null ||
                Threat.IsInterceptedOrExploded == true) return false;

            return _Threats.ContainsKey(Threat.ActiveID);
        }

        public async Task<bool> RemoveThreat(int ThreatId)
        {
            Threat? Threat = _context.Threat.Find(ThreatId);

            bool isRunning = IsThreatRunning(ThreatId);

            if (isRunning == false)
            {
                return false;
            }

            _Threats.TryRemove(Threat.ActiveID, out CancellationTokenSource? cts);
            cts?.Cancel();

            Threat.ActiveID = null;
            Threat.IsActive = false;
            Threat.IsInterceptedOrExploded = true;
            await _context.SaveChangesAsync();

            return true;
        }

        private async Task RunTask(string ThreatId, CancellationToken token)
        {
            try
            {
                int elapsed = 0;
                while (elapsed < 120 && !token.IsCancellationRequested)
                {
                    await Task.Delay(2000, token); // Wait for 2 seconds or cancel if requested
                    elapsed += 2;
                    var message = $"Threat {ThreatId} running for {elapsed} seconds.";
                    Console.WriteLine(message);
                    //await _hubContext.Clients.All.SendAsync("ReceiveProgress", message);
                }

                // Finished
                if (!token.IsCancellationRequested)
                {
                    //await _hubContext.Clients.All.SendAsync("ReceiveProgress", $"Threat {ThreatId} completed.");
                }
            }
            catch (TaskCanceledException)
            {
                //await _hubContext.Clients.All.SendAsync("ReceiveProgress", $"Threat {ThreatId} cancelled.");
            }
            finally
            {
                await RemoveThreat(int.Parse(ThreatId));
            }
        }

        internal void RebuildActiveThreats()
        {
            if (_Threats.Count > 0)
            {
                return;
            }

            var Threats = _context.Threat.Where(a => a.IsActive).ToList();
            foreach (var Threat in Threats)
            {
                if (Threat.ActiveID != null)
                {
                    var cts = new CancellationTokenSource();
                    _Threats[Threat.ActiveID] = cts;
                    _ = Task.Run(() => RunTask(Threat.ActiveID, cts.Token), cts.Token);
                }
            }
        }
    }
}
