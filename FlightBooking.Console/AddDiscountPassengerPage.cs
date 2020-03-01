using System;
using EasyConsoleCore;
using FlightBooking.Core;

namespace FlightBooking.Console
{
    public class AddDiscountPassengerPage : Page
    {
        public AddDiscountPassengerPage(EasyConsoleCore.Program program)
            : base("Add Passenger", program)
        {
        }
        
        public override void Display()
        {
            base.Display();
            
            var passengerName = Input.ReadString("Enter discount passenger name: ");

            var passengerAge = Input.ReadInt("Enter discount passenger age: ", 1, 120);

            var passenger = new DiscountedPassenger()
            {
                Name = passengerName, Age = passengerAge
            };
            
            FlightProgram.ScheduledFlight.AddPassenger(passenger);

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}