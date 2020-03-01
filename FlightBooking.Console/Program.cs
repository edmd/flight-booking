using System;
using System.Collections.Generic;
using System.Linq;
using FlightBooking.Core;

namespace FlightBooking.Console
{
    public class FlightProgram
    {
        // These fields would be loaded from persistent storage
        public static ScheduledFlight ScheduledFlight; 
        public static List<IPlane> Planes;
        public static List<IRule> Rules;

        private static void Main(string[] args)
        {
            SetupAirlineData();

            new FlightBookingProgram().Run();
        }

        private static void SetupAirlineData()
        {

            Planes = new List<IPlane>
            {
                new Plane { Id = 123, Name = "36 Bonanza", NumberOfSeats = 6 },
                new Plane { Id = 124, Name = "Piper PA-46", NumberOfSeats = 6 },
                new Plane { Id = 125, Name = "Cirrus Vision SF50 ", NumberOfSeats = 7 },
                new Plane { Id = 126, Name = "Cessna 208 Caravan", NumberOfSeats = 8 },
                new Plane { Id = 127, Name = "Antonov AN-2", NumberOfSeats = 12 },
                new Plane { Id = 128, Name = "DHC-8-102A", NumberOfSeats = 14 },
                new Plane { Id = 129, Name = "Viking DHC 6-400", NumberOfSeats = 19 },
                new Plane { Id = 130, Name = "Saab 340", NumberOfSeats = 34 },
                new Plane { Id = 131, Name = "Q400", NumberOfSeats = 68 }
            };
            
            var lhrCdgCorridor = new FlightRoute("London", "Paris")
            {
                BaseCost = 50, 
                BasePrice = 100, 
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.7
            };

            var ruleSet = new List<IRule>
            {
                new Rule { RuleName = "Profitable flight", RuleType = RuleType.Profitable, RuleEnabled = true, RulePriority = 1, ResponseCode = "01", ResponseMessage = "Flight not profitable" },
                new Rule { RuleName = "Flight Capacity", RuleType = RuleType.Capacity, RuleEnabled = true, RulePriority = 1, ResponseCode = "02", ResponseMessage = "Overbooked flight" },
                new Rule { RuleName = "Minimum Occupancy", RuleType = RuleType.MinimumOccupancy, RuleEnabled = true, RulePriority = 1, ResponseCode = "03", ResponseMessage = "Under booked flight" },
                new Rule { RuleName = "Airline Staff Override", RuleType = RuleType.AirlineStaffOverride, RuleEnabled = true, RulePriority = 1, ResponseCode = "04", ResponseMessage = "Not enough Airline staff" }
            };

            ScheduledFlight = new ScheduledFlight(lhrCdgCorridor, Planes.First(x => x.Id == 127), ruleSet);
        }
    }
}