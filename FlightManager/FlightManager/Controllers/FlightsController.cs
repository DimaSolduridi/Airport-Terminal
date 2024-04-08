using FinalProject.FlightManager.Models.Entities;
using FinalProject.FlightManager.Services;
using Microsoft.AspNetCore.Mvc;

namespace FlightManager.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class FlightsController : ControllerBase
    {
        private FlightService _flightService;

        public FlightsController(FlightService flightService)
        {
            _flightService=flightService;
        }
        [HttpPost]
        public void AddFlight(Flight newFlight)
        {
              _flightService.AddNewFlight(newFlight);     
        }
    }
}
