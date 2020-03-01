using System;
using EasyConsoleCore;
using FlightBooking.Core;

namespace FlightBooking.Console
{
    public class BulkLoadPassengerPage : Page
    {
        public BulkLoadPassengerPage(EasyConsoleCore.Program program)
            : base("Load Passengers", program)
        {
        }
        
        public override void Display()
        {
            base.Display();
            
            string command;
            do
            {
                System.Console.WriteLine("Please enter command.");
                command = System.Console.ReadLine() ?? "";
                
                try
                {
                    var enteredText = command.ToLower();
                    if (enteredText.Contains("add general"))
                    {
                        var passengerSegments = enteredText.Split(' ');
                        var generalPassenger = new GeneralPassenger
                        {
                            Name = passengerSegments[2],
                            Age = Convert.ToInt32(passengerSegments[3])
                        };
                        FlightProgram.ScheduledFlight.AddPassenger(generalPassenger);
                    }
                    else if (enteredText.Contains("add loyalty"))
                    {
                        var passengerSegments = enteredText.Split(' ');
                        var loyaltyPassenger = new LoyaltyPassenger
                        {
                            Name = passengerSegments[2],
                            Age = Convert.ToInt32(passengerSegments[3]),
                            TotalPoints = Convert.ToInt32(passengerSegments[4]),
                            UsePoints = Convert.ToBoolean(passengerSegments[5])
                        };
                        FlightProgram.ScheduledFlight.AddPassenger(loyaltyPassenger);
                    }
                    else if (enteredText.Contains("add airline"))
                    {
                        var passengerSegments = enteredText.Split(' ');
                        var airlineEmployee = new AirlineEmployee
                        {
                            Name = passengerSegments[2],
                            Age = Convert.ToInt32(passengerSegments[3])
                        };
                        FlightProgram.ScheduledFlight.AddPassenger(airlineEmployee);
                    }
                    else if (enteredText.Contains("add discount"))
                    {
                        var passengerSegments = enteredText.Split(' ');
                        var discountEmployee = new DiscountedPassenger
                        {
                            Name = passengerSegments[2],
                            Age = Convert.ToInt32(passengerSegments[3])
                        };
                        FlightProgram.ScheduledFlight.AddPassenger(discountEmployee);
                    }
                    else if (enteredText.Contains("display summary"))
                    {
                        Program.NavigateTo<DisplaySummaryPage>();
                    }
                }
                catch
                {
                    Output.WriteLine("Input format was not in a correct format. Please try again.");
                }
            } while (command != "exit");
            
            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}