using FlightManager.Models.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace FinalProject.FlightManager.Models.Entities
{
    public class Log
    {
        public int Id { get; set; }
        public virtual Flight Flight { get; set; }
        public virtual Leg Leg { get; set; }
        public FlightStatus Status { get; set; }
        public DateTime? EnterLeg { get; set; }
        public DateTime? ExitLeg { get; set; }
    }
}
