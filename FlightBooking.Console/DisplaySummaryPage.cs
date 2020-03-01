using System;
using System.Linq;
using EasyConsoleCore;
using FlightBooking.Core;

namespace FlightBooking.Console
{
    public class DisplaySummaryPage : Page
    {
        private readonly string _verticalWhiteSpace = Environment.NewLine + Environment.NewLine;
        private readonly string _newLine = Environment.NewLine;
        private const string Indentation = "    ";
        
        public DisplaySummaryPage(EasyConsoleCore.Program program)
            : base("Summary Page", program)
        {
        }

        public override void Display()
        {
            FlightProgram.ScheduledFlight.CalculateFlightDetails();
                
            base.Display();
            
            var result = "Flight summary for " + FlightProgram.ScheduledFlight.FlightRoute;

            result += _verticalWhiteSpace;
            
            result += "Total passengers: " + FlightProgram.ScheduledFlight.Passengers.Count;
            result += _newLine;
            result += Indentation + "General sales: " + FlightProgram.ScheduledFlight.Passengers.OfType<GeneralPassenger>().Count();
            result += _newLine;
            result += Indentation + "Loyalty member sales: " + FlightProgram.ScheduledFlight.Passengers.OfType<LoyaltyPassenger>().Count();
            result += _newLine;
            result += Indentation + "Airline employee comps: " + FlightProgram.ScheduledFlight.Passengers.OfType<AirlineEmployee>().Count();
            result += _newLine;
            result += Indentation + "Discount passenger: " + FlightProgram.ScheduledFlight.Passengers.OfType<DiscountedPassenger>().Count();
            
            result += _verticalWhiteSpace;
            result += "Total expected baggage: " + FlightProgram.ScheduledFlight.TotalExpectedBaggage;

            result += _verticalWhiteSpace;

            result += "Total revenue from flight: " + FlightProgram.ScheduledFlight.ProfitFromFlight;
            result += _newLine;
            result += "Total costs from flight: " + FlightProgram.ScheduledFlight.CostOfFlight;
            result += _newLine;

            result += (FlightProgram.ScheduledFlight.NetProfit > 0 ? "Flight generating profit of: " : "Flight losing money of: ") + FlightProgram.ScheduledFlight.NetProfit;

            result += _verticalWhiteSpace;

            result += "Total loyalty points given away: " + FlightProgram.ScheduledFlight.TotalLoyaltyPointsAccrued + _newLine;
            result += "Total loyalty points redeemed: " + FlightProgram.ScheduledFlight.TotalLoyaltyPointsRedeemed + _newLine;

            result += _verticalWhiteSpace;

            if (FlightProgram.ScheduledFlight.ConfirmFlightViability())
            {
                result += "THIS FLIGHT MAY PROCEED";
                Output.WriteLine(result);
            }
            else
            {
                result += "FLIGHT MAY NOT PROCEED"  + _newLine;
                result += "Other more suitable aircraft are: " + _newLine;

                result = FlightProgram.Planes.Where(x => x.NumberOfSeats >= FlightProgram.ScheduledFlight.Passengers.Count).Aggregate(result, (current, plane) => current + (plane.Name + _newLine));

                Output.WriteLine(result);
            }

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}