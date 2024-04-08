namespace FinalProject.FlightManager.Models.Entities
{
    public class AvailableLeg
    {
        public int Id { get; set; }
        public virtual Leg? From { get; set; }
        public virtual Leg? To { get; set; }
    }
}
