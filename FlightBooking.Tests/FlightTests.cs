using System;
using System.Collections.Generic;
using System.Linq;
using FlightBooking.Core;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace FlightBooking.Tests
{
    [TestFixture]
    public class Tests
    {
        private FlightRoute _flightRoute;
        private ScheduledFlight _scheduledFlight;
        private List<IPlane> _planes;
        //TODO: Add mock tests for Rules

        [SetUp]
        public void Setup()
        {
            _flightRoute = new FlightRoute("London", "Paris")
            {
                BasePrice = 100,
                BaseCost = 50,
                LoyaltyPointsGained = 5,
                MinimumTakeOffPercentage = 0.5D
            };

            _planes = new List<IPlane>
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

            var ruleSet = new List<IRule>
            {
                new Rule { RuleName = "Profitable flight", RuleType = RuleType.Profitable, RuleEnabled = true, RulePriority = 1, ResponseCode = "01", ResponseMessage = "Flight not profitable" },
                new Rule { RuleName = "Flight Capacity", RuleType = RuleType.Capacity, RuleEnabled = true, RulePriority = 1, ResponseCode = "02", ResponseMessage = "Overbooked flight" },
                new Rule { RuleName = "Minimum Occupancy", RuleType = RuleType.MinimumOccupancy, RuleEnabled = true, RulePriority = 1, ResponseCode = "03", ResponseMessage = "Under booked flight" },
                new Rule { RuleName = "Airline Staff Override", RuleType = RuleType.AirlineStaffOverride, RuleEnabled = true, RulePriority = 1, ResponseCode = "04", ResponseMessage = "Not enough Airline staff" }
            };

            _scheduledFlight = new ScheduledFlight(_flightRoute, _planes.First(x => x.Id == 127), ruleSet);
        }

        [Test]
        public void MinimumTakeOffPercentageTest()
        {
            var passengers = new List<IPassenger>();
            
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());

            _scheduledFlight.Passengers = passengers;
            _scheduledFlight.CalculateFlightDetails();
            
            // 5 Passengers
            _scheduledFlight.ConfirmFlightViability().Should().BeFalse();
            _scheduledFlight.NetProfit.Should().Be(250);
            _scheduledFlight.CostOfFlight.Should().Be(250);
            _scheduledFlight.ProfitFromFlight.Should().Be(500);
            _scheduledFlight.TotalExpectedBaggage.Should().Be(5);
            _scheduledFlight.TotalLoyaltyPointsAccrued.Should().Be(0);
            _scheduledFlight.TotalLoyaltyPointsRedeemed.Should().Be(0);
            
            _scheduledFlight.AddPassenger(GetGeneralPassenger());
            _scheduledFlight.CalculateFlightDetails();

            // 6 Passengers
            _scheduledFlight.ConfirmFlightViability().Should().BeTrue();
            _scheduledFlight.NetProfit.Should().Be(300);
            _scheduledFlight.CostOfFlight.Should().Be(300);
            _scheduledFlight.ProfitFromFlight.Should().Be(600);
            _scheduledFlight.TotalExpectedBaggage.Should().Be(6);
            _scheduledFlight.TotalLoyaltyPointsAccrued.Should().Be(0);
            _scheduledFlight.TotalLoyaltyPointsRedeemed.Should().Be(0);

            _scheduledFlight.AddPassenger(GetGeneralPassenger());
            _scheduledFlight.CalculateFlightDetails();
            
            // 7 Passengers
            _scheduledFlight.ConfirmFlightViability().Should().BeTrue();
            _scheduledFlight.NetProfit.Should().Be(350);
            _scheduledFlight.CostOfFlight.Should().Be(350);
            _scheduledFlight.ProfitFromFlight.Should().Be(700);
            _scheduledFlight.TotalExpectedBaggage.Should().Be(7);
            _scheduledFlight.TotalLoyaltyPointsAccrued.Should().Be(0);
            _scheduledFlight.TotalLoyaltyPointsRedeemed.Should().Be(0);
        }

        [Test]
        public void FlightOverBookedTest()
        {
            var passengers = new List<IPassenger>();
            
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());
            passengers.Add(GetGeneralPassenger());

            _scheduledFlight.Passengers = passengers;
            _scheduledFlight.CalculateFlightDetails();
            
            // 13 Passengers
            _scheduledFlight.ConfirmFlightViability().Should().BeFalse();
        }
        
        [Test]
        public void ZeroNetProfitTest()
        {
            var passengers = new List<IPassenger>();
            
            passengers.Add(GetDiscountedPassenger());
            passengers.Add(GetDiscountedPassenger());
            passengers.Add(GetDiscountedPassenger());
            passengers.Add(GetDiscountedPassenger());
            passengers.Add(GetDiscountedPassenger());
            passengers.Add(GetDiscountedPassenger());
            

            _scheduledFlight.Passengers = passengers;
            _scheduledFlight.CalculateFlightDetails();
            
            // 6 Passengers
            _scheduledFlight.ConfirmFlightViability().Should().BeFalse();
            _scheduledFlight.NetProfit.Should().Be(0);
            _scheduledFlight.CostOfFlight.Should().Be(300);
            _scheduledFlight.ProfitFromFlight.Should().Be(300);
            _scheduledFlight.TotalExpectedBaggage.Should().Be(0);
            _scheduledFlight.TotalLoyaltyPointsAccrued.Should().Be(0);
            _scheduledFlight.TotalLoyaltyPointsRedeemed.Should().Be(0);
        }
        
        [Test]
        public void PositiveNetProfitTest()
        {
            var passengers = new List<IPassenger>();
            
            passengers.Add(GetDiscountedPassenger());
            passengers.Add(GetDiscountedPassenger());
            passengers.Add(GetDiscountedPassenger());
            passengers.Add(GetDiscountedPassenger());
            passengers.Add(GetDiscountedPassenger());
            passengers.Add(GetDiscountedPassenger());
            passengers.Add(GetGeneralPassenger());
            

            _scheduledFlight.Passengers = passengers;
            _scheduledFlight.CalculateFlightDetails();
            
            // 7 Passengers
            _scheduledFlight.ConfirmFlightViability().Should().BeTrue();
            _scheduledFlight.NetProfit.Should().Be(50);
            _scheduledFlight.CostOfFlight.Should().Be(350);
            _scheduledFlight.ProfitFromFlight.Should().Be(400);
            _scheduledFlight.TotalExpectedBaggage.Should().Be(1);
            _scheduledFlight.TotalLoyaltyPointsAccrued.Should().Be(0);
            _scheduledFlight.TotalLoyaltyPointsRedeemed.Should().Be(0);
        }
        
        [Test]
        public void AllowAirlineEmployeesRuleTest()
        {
            var passengers = new List<IPassenger>();
            
            passengers.Add(GetAirlineEmployee());
            passengers.Add(GetAirlineEmployee());
            passengers.Add(GetAirlineEmployee());
            passengers.Add(GetAirlineEmployee());
            passengers.Add(GetAirlineEmployee());
            passengers.Add(GetAirlineEmployee());


            _scheduledFlight.Passengers = passengers;
            _scheduledFlight.CalculateFlightDetails();
            
            // 6 Passengers
            _scheduledFlight.ConfirmFlightViability().Should().BeTrue();
            _scheduledFlight.NetProfit.Should().Be(-300);
            _scheduledFlight.CostOfFlight.Should().Be(300);
            _scheduledFlight.ProfitFromFlight.Should().Be(0);
            _scheduledFlight.TotalExpectedBaggage.Should().Be(6);
            _scheduledFlight.TotalLoyaltyPointsAccrued.Should().Be(0);
            _scheduledFlight.TotalLoyaltyPointsRedeemed.Should().Be(0);
            
            
            // Turn off Airline Employee rule
            _scheduledFlight.Rules.Remove(
                _scheduledFlight.Rules.First(r => r.RuleType == RuleType.AirlineStaffOverride));

            _scheduledFlight.ConfirmFlightViability().Should().BeFalse();
        }

        [Test]
        public void LoyaltyFullyBookedTest()
        {
            var passengers = new List<IPassenger>();
            
            passengers.Add(GetLoyaltyPassenger(false));
            passengers.Add(GetLoyaltyPassenger(false));
            passengers.Add(GetLoyaltyPassenger(false));
            passengers.Add(GetLoyaltyPassenger(false));
            passengers.Add(GetLoyaltyPassenger(false));
            passengers.Add(GetLoyaltyPassenger(false));
            passengers.Add(GetLoyaltyPassenger(false));
            passengers.Add(GetLoyaltyPassenger(false));
            passengers.Add(GetLoyaltyPassenger(false));
            passengers.Add(GetLoyaltyPassenger(false));
            passengers.Add(GetLoyaltyPassenger(false));
            passengers.Add(GetLoyaltyPassenger(false));


            _scheduledFlight.Passengers = passengers;
            _scheduledFlight.CalculateFlightDetails();
            
            // 12 Passengers
            _scheduledFlight.ConfirmFlightViability().Should().BeTrue();
            _scheduledFlight.NetProfit.Should().Be(600);
            _scheduledFlight.CostOfFlight.Should().Be(600);
            _scheduledFlight.ProfitFromFlight.Should().Be(1200);
            _scheduledFlight.TotalExpectedBaggage.Should().Be(24);
            _scheduledFlight.TotalLoyaltyPointsAccrued.Should().Be(60);
            _scheduledFlight.TotalLoyaltyPointsRedeemed.Should().Be(0);
        }
        
        [Test]
        public void LoyaltyPointsBookedTest()
        {
            var passengers = new List<IPassenger>();
            
            passengers.Add(GetLoyaltyPassenger(true));
            passengers.Add(GetLoyaltyPassenger(true));
            passengers.Add(GetLoyaltyPassenger(true));
            passengers.Add(GetLoyaltyPassenger(true));
            passengers.Add(GetLoyaltyPassenger(true));
            passengers.Add(GetLoyaltyPassenger(true));
            passengers.Add(GetLoyaltyPassenger(true));
            passengers.Add(GetLoyaltyPassenger(true));
            passengers.Add(GetLoyaltyPassenger(true));
            passengers.Add(GetLoyaltyPassenger(true));
            passengers.Add(GetLoyaltyPassenger(true));
            passengers.Add(GetLoyaltyPassenger(true));


            _scheduledFlight.Passengers = passengers;
            _scheduledFlight.CalculateFlightDetails();
            
            // 12 Passengers
            _scheduledFlight.ConfirmFlightViability().Should().BeFalse();
            _scheduledFlight.NetProfit.Should().Be(-600);
            _scheduledFlight.CostOfFlight.Should().Be(600);
            _scheduledFlight.ProfitFromFlight.Should().Be(0);
            _scheduledFlight.TotalExpectedBaggage.Should().Be(24);
            _scheduledFlight.TotalLoyaltyPointsAccrued.Should().Be(0);
            _scheduledFlight.TotalLoyaltyPointsRedeemed.Should().Be(1200);
        }
        
        #region Helper methods...
        private static AirlineEmployee GetAirlineEmployee()
        {
            return new AirlineEmployee
            {
                Name = Guid.NewGuid().ToString("n").Substring(0, 8),
                Age = new Random().Next(1, 120)
            };
        }
        
        private static DiscountedPassenger GetDiscountedPassenger()
        {
            return new DiscountedPassenger
            {
                Name = Guid.NewGuid().ToString("n").Substring(0, 8),
                Age = new Random().Next(1, 120)
            };
        }
        
        private static GeneralPassenger GetGeneralPassenger()
        {
            return new GeneralPassenger
            {
                Name = Guid.NewGuid().ToString("n").Substring(0, 8),
                Age = new Random().Next(1, 120)
            };
        }
        
        private static  LoyaltyPassenger GetLoyaltyPassenger(bool usePoints)
        {
            return new LoyaltyPassenger()
            {
                Name = Guid.NewGuid().ToString("n").Substring(0, 8),
                Age = new Random().Next(1, 120),
                TotalPoints = new Random().Next(100, 200),
                UsePoints = usePoints
            };
        }
        #endregion
    }
}