using FinalProject.FlightManager.Hubs;
using FinalProject.FlightManager.Models.Entities;
using FinalProject.FlightManager.Models.Enums;
using FlightManager.Data.Contexts;
using FlightManager.Models.Enums;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;

namespace FinalProject.FlightManager.Services
{
    public class FlightService
    {
        private readonly DataContext _data;
        private List<Leg> _legs;
        private List<AvailableLeg> _availableLegs;
        private int FullFlightsCapacity = 4;
        private int TimeToWait = 3000;
        private readonly IHubContext<AirportHub> _hubContext;

        public FlightService(IServiceScopeFactory scopeService, IHubContext<AirportHub> hubContext)
        {
            _data = scopeService.CreateScope().ServiceProvider.GetRequiredService<DataContext>();
            _hubContext = hubContext;
            _legs = _data.Legs.ToList();
            _availableLegs = _data.AvailableLegs
                .Include(al => al.From)
                .Include(al => al.To).ToList();
        }

        public void AddNewFlight(Flight flight)
        {
            if (!IsLegOccupied(GetFirstLeg()) && !IsTerminalFull())
            {
                SetInitialFlightStatus(flight);
                flight.CurrentLeg = GetLeg(GetFirstLeg());
                flight.CurrentLeg.IsOccupied = true;
                AddFlight(flight);
                AddLog(flight);
                Save();
                flight.CrossingLegEnded += MoveToNextLegs;
            }

        }



        private void MoveToNextLegs(Flight flight)
        {

            StopTimer(flight);
            flight.CrossingLegEnded -= MoveToNextLegs;
            if (!IsDepartured(flight))
            {
                ManageMovementLogic(flight);
            }
        }


        private void ManageMovementLogic(Flight flight)
        {

            var availableLegs = GetNextLegs(flight);
            bool foundNonOccupiedLeg = false;
            foreach (var leg in availableLegs)
            {
                lock (this)
                {
                    if (!IsLegOccupied(leg.Number))
                    {
                        ChangeLeg(leg, flight);
                        foundNonOccupiedLeg = true;
                        break;
                    }
                }
            }
            if (!foundNonOccupiedLeg)
            {
                SetTimerToWait(flight);
                flight.CrossingLegEnded += MoveToNextLegs;
            }

        }

        private void ChangeLeg(Leg leg, Flight flight)
        {
            if (leg.LegStage == LegStage.Boarding) ChangeFlightStatusToDeparture(flight);
            if (leg.Number != _legs.Last().Number)
            {
                UpdateOutTimeInLog(flight);
                flight.CurrentLeg.IsOccupied = false;
                flight.CurrentLeg = leg;
                flight.CurrentLeg.IsOccupied = true;
                AddLog(flight);
                Save();
                flight.CrossingLegEnded += MoveToNextLegs;
            }
            else
            {
                HandleDepartingFlight(flight, leg);
            }

        }

        private void HandleDepartingFlight(Flight flight, Leg leg)
        {
            StopTimer(flight);
            flight.CrossingLegEnded -= MoveToNextLegs;
            flight.CurrentLeg.IsOccupied = false;
            UpdateOutTimeInLog(flight);
            flight.CurrentLeg = leg;
            SetFlightAsDepartured(flight);
            AddLog(flight);
            Save();
        }

        private ICollection<Leg> GetNextLegs(Flight flight)
        {
            var nextLegs = _availableLegs.Where(al => al.To != null && al.From.Id == flight.CurrentLeg.Id).Select(leg => leg.To).ToList();
            if (flight.FlightStatus == FlightStatus.Arrival)
            {
                return nextLegs.FindAll(next => next.LegStatus.HasFlag(LegStatus.Arrival) || next.LegStatus.HasFlag(LegStatus.Both));
            }
            else
            {
                return nextLegs.FindAll(next => next.LegStatus.HasFlag(LegStatus.Departure) || next.LegStatus.HasFlag(LegStatus.Both));
            }
        }

        private void AddLog(Flight flight)
        {
            var log = new Log { Flight = flight, Leg = flight.CurrentLeg, EnterLeg = DateTime.Now };
             _data.Logs.Add(log);
            _hubContext.Clients.All.SendAsync("UpdateLogs", log).Wait();


        }

        private void UpdateOutTimeInLog(Flight flight)
        {
            var lastLog = _data.Logs.Where(l => l.Flight.Id == flight.Id)
           .OrderByDescending(l => l.Id)
           .FirstOrDefault();
            if (lastLog != null)
            {
                lastLog.ExitLeg = DateTime.Now;
                _hubContext.Clients.All.SendAsync("UpdateLogs", lastLog).Wait();
            }
        }



        //TODO:add null reference checks and validations
        private void Save() => _data.SaveChanges();
        private void AddFlight(Flight flight) => _data.Flights.Add(flight);
        private bool IsLegOccupied(int legNumber) => _data.Legs.Any(l => l.Number == legNumber && l.IsOccupied == true);
        private bool IsTerminalFull() => _data.Legs.Count(leg => leg.IsOccupied == true) >= FullFlightsCapacity;
        private void SetInitialFlightStatus(Flight flight) => flight.FlightStatus = FlightStatus.Arrival;
        private void SetFlightAsDepartured(Flight flight) => flight.FlightStatus = FlightStatus.Departured;
        private bool IsDepartured(Flight flight) => flight.CurrentLeg.Number == _legs.Last().Number || flight.FlightStatus == FlightStatus.Departured;
        private void ChangeFlightStatusToDeparture(Flight flight) => flight.FlightStatus = FlightStatus.Departure;
        private Leg GetLeg(int legNumber) => _legs.First(l => l.Number == legNumber);
        private int GetFirstLeg() => _legs.First().Number;
        private void StopTimer(Flight flight) => flight.LegTimer.Change(Timeout.Infinite, Timeout.Infinite);
        private void SetTimerToWait(Flight flight) => flight.LegTimer.Change(0, TimeToWait);


    }
}
