using Microsoft.AspNetCore.SignalR;
using DefenseSimulator.Models;
using NuGet.Protocol;
using DefenseSimulator.Services;
namespace DefenseSimulator.Hubs
{
    public class NotificationHub : Hub
    {
        private readonly ThreatHandlerService _threatHandlerService;
        public NotificationHub(ThreatHandlerService threatHandlerService)
        { 
            _threatHandlerService = threatHandlerService;
        }
        public async Task LaunchThreat(Threat threat)
        {
            await Clients.All.SendAsync("ReceiveProgress", threat);
        }
        
        public async Task InterseptThreat(int arsenalId)
        {
            await _threatHandlerService.InterseptThreat(arsenalId);
        }
    }
}
