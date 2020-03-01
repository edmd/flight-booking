using System;
using EasyConsoleCore;

namespace FlightBooking.Console
{
    public class FlightBookingProgram : Program
    {
        public FlightBookingProgram()
            : base("Flight Bookings", false)
        {
            AddPage(new MainMenuPage(this));
            //AddPage(new AddGeneralPassengerPage(this));
            //AddPage(new AddAirlinePassengerPage(this));
			//AddPage(new AddLoyaltyPassengerPage(this));
			//AddPage(new AddDiscountPassengerPage(this));
            AddPage(new BulkLoadPassengerPage(this));
            AddPage(new DisplaySummaryPage(this));

            SetPage<MainMenuPage>();
        }
    }
}