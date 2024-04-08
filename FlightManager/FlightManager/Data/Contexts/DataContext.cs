using FinalProject.FlightManager.Models.Entities;
using FinalProject.FlightManager.Models.Enums;
using FlightManager.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FlightManager.Data.Contexts
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public virtual DbSet<Flight> Flights { get; set; }
        public virtual DbSet<Leg> Legs { get; set; }
        public virtual DbSet<Log> Logs { get; set; }
        public virtual DbSet<AvailableLeg> AvailableLegs { get; set; }

        public void SeedData(DataContext context)
        { 
            var l1 = new Leg { Number = 1, CrossingTime = 4000,LegStatus=LegStatus.Arrival,LegStage=LegStage.AprroachToLand};
            var l2 = new Leg { Number = 2, CrossingTime = 4000, LegStatus = LegStatus.Arrival, LegStage = LegStage.AprroachToLand };
            var l3 = new Leg { Number = 3, CrossingTime = 4000, LegStatus = LegStatus.Arrival, LegStage = LegStage.AprroachToLand };
            var l4 = new Leg { Number = 4, CrossingTime = 12000, LegStatus = LegStatus.Both, LegStage = LegStage.Land };
            var l5 = new Leg { Number = 5, CrossingTime = 6000, LegStatus = LegStatus.Arrival, LegStage = LegStage.TaxiToPassangerEvacution };
            var l6 = new Leg { Number = 6, CrossingTime = 12000, LegStatus = LegStatus.Both, LegStage = LegStage.Boarding };
            var l7 = new Leg { Number = 7, CrossingTime = 12000 , LegStatus = LegStatus.Arrival, LegStage = LegStage.Boarding };
            var l8 = new Leg { Number = 8, CrossingTime = 4000,LegStatus = LegStatus.Departure, LegStage = LegStage.TaxiToDeparture };
            var l9 = new Leg { Number = 9, CrossingTime = 999999999, LegStatus = LegStatus.Departure, LegStage = LegStage.Departure };


            var al1 = new AvailableLeg { From = l1, To = l2 };
            var al2 = new AvailableLeg { From = l2, To = l3 };
            var al3 = new AvailableLeg { From = l3, To = l4 };
            var al4 = new AvailableLeg {From = l4, To = l5 };
            var al5 = new AvailableLeg {From = l4, To = l9 };
            var al6 = new AvailableLeg { From = l5, To = l6 };
            var al7 = new AvailableLeg { From = l5, To = l7 };
            var al8 = new AvailableLeg { From = l6, To = l8 };
            var al9 = new AvailableLeg { From = l7, To = l8 };
            var al10 = new AvailableLeg { From = l8, To = l4 };
            var al11 = new AvailableLeg { From = l9, To = null };



            if (!context.Legs.Any())
            {
                context.Legs.AddRange(l1, l2, l3, l4, l5, l6, l7, l8,l9);
                context.SaveChanges();
            }

            

            if (!context.AvailableLegs.Any())
            {
                context.AvailableLegs.AddRange(al1, al2, al3, al4, al5, al6, al7, al8,al9,al10,al11);
                context.SaveChanges();
            }

            
        }
    }
}
