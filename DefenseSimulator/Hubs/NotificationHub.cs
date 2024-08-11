using Microsoft.AspNetCore.SignalR;
using DefenseSimulator.Models;
using NuGet.Protocol;
namespace DefenseSimulator.Hubs
{
    public class NotificationHub : Hub
    {
        public async Task LaunchThreat(Threat threat)
        {
            await Clients.All.SendAsync("ReceiveThreat", threat);
        }

    }
}
