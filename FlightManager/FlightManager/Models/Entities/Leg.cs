

using FinalProject.FlightManager.Models.Enums;
using System.Diagnostics;


namespace FinalProject.FlightManager.Models.Entities
{
    public class Leg
    {
        public int Id { get; set; }
        public int Number { get; set; }
        public int CrossingTime { get; set; }
        public LegStatus LegStatus { get; set; }
        public LegStage LegStage { get; set; }
        public bool IsOccupied {  get; set; }
      
   
    }
}
