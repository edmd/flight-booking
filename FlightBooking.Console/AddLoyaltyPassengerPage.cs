using System;
using EasyConsoleCore;
using FlightBooking.Core;

namespace FlightBooking.Console
{
    public class AddLoyaltyPassengerPage : Page
    {
        public AddLoyaltyPassengerPage(EasyConsoleCore.Program program)
            : base("Add Loyalty Passenger", program)
        {
        }
        
        public override void Display()
        {
            base.Display();
            
            var passengerName = Input.ReadString("Enter Loyalty passenger name: ");

            var passengerAge = Input.ReadInt("Enter Loyalty passenger age: ", 1, 120);
            
            var passengerPoints = Input.ReadInt("Enter Loyalty passenger points total: ", 1, int.MaxValue);

            var passengerUsePoints = Input.ReadBool("Loyalty passenger using points?");

            var passenger = new LoyaltyPassenger()
            {
                Name = passengerName, Age = passengerAge, TotalPoints = passengerPoints, UsePoints = passengerUsePoints
            };
            
            FlightProgram.ScheduledFlight.AddPassenger(passenger);

            Input.ReadString("Press [Enter] to navigate home");
            Program.NavigateHome();
        }
    }
}