using System;
using System.Linq;
using System.Collections.Generic;

namespace FlightBooking.Core
{
    public class ScheduledFlight
    {
        public IFlightRoute FlightRoute { get; set; }
        public IPlane Aircraft { get; set; }
        public List<IPassenger> Passengers { get; set; }
        public List<IRule> Rules { get; set; }

        public double ProfitFromFlight { get; private set; }
        public double CostOfFlight { get; private set; }
        public double NetProfit => ProfitFromFlight - CostOfFlight;
        public int TotalLoyaltyPointsAccrued { get; private set; }
        public int TotalLoyaltyPointsRedeemed { get; private set; }
        public int TotalExpectedBaggage { get; private set; }

        public ScheduledFlight(IFlightRoute flightRoute, IPlane aircraft, List<IRule> rules)
        {
            Aircraft = aircraft;
            FlightRoute = flightRoute;
            Rules = rules;
            //RuleEngine = ruleEngine;
            
            Passengers = new List<IPassenger>();

            ProfitFromFlight = 0;
            CostOfFlight = 0;
            TotalLoyaltyPointsAccrued = 0;
            TotalLoyaltyPointsRedeemed = 0;
            TotalExpectedBaggage = 0;
        }

        public void AddPassenger(IPassenger passenger)
        {
            // Potentially we could calculate load before adding passenger - but the corridor to plane relationship is amorphous
            Passengers.Add(passenger);
        }

        // calculate dynamically
        public void CalculateFlightDetails()
        {
            CostOfFlight = Passengers.Count * FlightRoute.BaseCost;
            ProfitFromFlight = Passengers.Sum(x => x.GetPrice(FlightRoute.BasePrice, FlightRoute.LoyaltyPointsGained));
            TotalExpectedBaggage = Passengers.Sum(x => x.AllowedBags);

            TotalLoyaltyPointsRedeemed = Passengers.OfType<LoyaltyPassenger>().Count(x => x.UsePoints) 
                                        *  Convert.ToInt32(Math.Ceiling(FlightRoute.BasePrice));

            TotalLoyaltyPointsAccrued = Passengers.OfType<LoyaltyPassenger>().Count(x => x.UsePoints == false)
                                         * FlightRoute.LoyaltyPointsGained;
        }
        
        public bool ConfirmFlightViability()
        {
            // Is the flight allowed to proceed
            return RulesEngine.ApplyRules(this);
        }
    }
}
