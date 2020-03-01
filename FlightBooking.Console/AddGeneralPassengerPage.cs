using EasyConsoleCore;
using FlightBooking.Core;

namespace FlightBooking.Console
{
    public class AddGeneralPassengerPage : Page
    {
        public AddGeneralPassengerPage(EasyConsoleCore.Program program)
            : base("Add General Passenger", program)
        {
        }
        
        public override void Display()
        {
            base.Display();
            
            var passengerName = Input.ReadString("Enter general passenger name: ");

            var passengerAge = Input.ReadInt("Enter general passenger age: ", 1, 120);

            var passenger = new GeneralPassenger()
            {
                Name = passengerName, Age = passengerAge
            };
            
            FlightProgram.ScheduledFlight.AddPassenger(passenger);

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}