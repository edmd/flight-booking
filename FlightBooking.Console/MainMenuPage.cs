using EasyConsoleCore;

namespace FlightBooking.Console
{
    internal class MainMenuPage : MenuPage
    {
        public MainMenuPage(EasyConsoleCore.Program program)
            : base("Main Page", program, 
                /*new Option("Add General Passenger", () => program.NavigateTo<AddGeneralPassengerPage>()),
				new Option("Add Loyalty Passenger", () => program.NavigateTo<AddLoyaltyPassengerPage>()),
				new Option("Add Airline Passenger", () => program.NavigateTo<AddAirlinePassengerPage>()),
				new Option("Add Discount Passenger", () => program.NavigateTo<AddDiscountPassengerPage>()),*/
                new Option("Load Passengers", () => program.NavigateTo<BulkLoadPassengerPage>()),
				new Option("Display Summary", () => program.NavigateTo<DisplaySummaryPage>()))
        {
        }
    }
}