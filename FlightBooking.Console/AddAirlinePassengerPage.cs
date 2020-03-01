using EasyConsoleCore;
using FlightBooking.Core;

namespace FlightBooking.Console
{
    public class AddAirlinePassengerPage : Page
    {
        public AddAirlinePassengerPage(EasyConsoleCore.Program program)
            : base("Add Airline Passenger", program)
        {
        }
        
        public override void Display()
        {
            base.Display();
            
            var passengerName = Input.ReadString("Enter Airline passenger name: ");

            var passengerAge = Input.ReadInt("Enter Airline passenger age: ", 1, 120);

            var passenger = new AirlineEmployee
            {
                Name = passengerName, Age = passengerAge
            };
            
            FlightProgram.ScheduledFlight.AddPassenger(passenger);

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}