using FinalProject.FlightManager.Models.Entities;
using Microsoft.AspNetCore.SignalR;

namespace FinalProject.FlightManager.Hubs
{
    public class AirportHub : Hub
    {
        public async Task  UpdateLogs(Log log)
        {
              await Clients.All.SendAsync("UpdateLogs", log);
        }
    }
}
