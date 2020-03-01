using System;
using System.Collections.Generic;
using System.Linq;

namespace FlightBooking.Core
{
    // We can apply cascading rules by specifying a global set of rules here and overriding each policy with more
    // domain specific (i.e. FlightRoute) rules if present, rules need to be genericized further
    // TODO: Potentially change this to a factory - it would be injected on start
    public static class RulesEngine
    {
        public static bool ApplyRules(ScheduledFlight scheduledFlight)
        {
            foreach (var rule in scheduledFlight.Rules.Where(rule => rule.RuleEnabled))
            {
                switch (rule.RuleType)
                {
                    case RuleType.Capacity:
                        if (scheduledFlight.Passengers.Count > scheduledFlight.Aircraft.NumberOfSeats)
                        {
                            return false;
                        }
                        break;
                    case RuleType.Profitable:
                        if (scheduledFlight.NetProfit <= 0)
                        {
                            if (!scheduledFlight.Rules.Any(r => r.RuleEnabled && r.RuleType == RuleType.AirlineStaffOverride))
                            {
                                return false;
                            }
                            
                            if(scheduledFlight.Rules.Any(r => r.RuleEnabled && r.RuleType == RuleType.AirlineStaffOverride) 
                                    && scheduledFlight.Passengers.OfType<AirlineEmployee>().Count() / (double) scheduledFlight.Aircraft.NumberOfSeats < 
                                    scheduledFlight.FlightRoute.MinimumTakeOffPercentage)
                            {
                                return false;
                            }
                            
                        }
                        break;
                    case RuleType.AirlineStaffOverride:
                        if (scheduledFlight.Passengers.Count / (double) scheduledFlight.Aircraft.NumberOfSeats <
                            scheduledFlight.FlightRoute.MinimumTakeOffPercentage &&
                            scheduledFlight.Passengers.OfType<AirlineEmployee>().Count() /
                            (double) scheduledFlight.Aircraft.NumberOfSeats <
                            scheduledFlight.FlightRoute.MinimumTakeOffPercentage)
                            return false;
                        break;
                    case RuleType.MinimumOccupancy:
                        if (scheduledFlight.Passengers.Count / (double) scheduledFlight.Aircraft.NumberOfSeats <
                            scheduledFlight.FlightRoute.MinimumTakeOffPercentage)
                        {
                            return false;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                };
            }
            
            return true;
        }
    }
}