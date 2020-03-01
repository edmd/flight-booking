namespace FlightBooking.Core
{
    public class AirlineEmployee : Passenger
    {
        public override double GetPrice(double price, int flightAwardedPoints) {
			return 0;
		}
    }
}