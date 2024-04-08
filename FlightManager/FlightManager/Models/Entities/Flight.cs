using FinalProject.FlightManager.Services;
using FlightManager.Models.Enums;


namespace FinalProject.FlightManager.Models.Entities
{
    public class Flight
    {
        public int Id { get; set; }
        public string Number { get; set; }
        public FlightStatus FlightStatus { get; set; }
        public int PassengersCount { get; set; }
        public Timer LegTimer;
        private Leg? currentLeg;
        public Leg? CurrentLeg
        {
            get => currentLeg; set
            {
                currentLeg = value;

                if (currentLeg != null)
                {
                    LegTimer.Change(0, currentLeg.CrossingTime);

                }
            }
        }

        public Flight()
        {
            LegTimer = new Timer(MyCallback);
        }

        private void MyCallback(object? state)
        {
            CrossingLegEnded?.Invoke(this);
        }


        public event Action<Flight>? CrossingLegEnded;
        


       

}   }
