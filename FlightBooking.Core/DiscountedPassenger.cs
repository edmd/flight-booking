namespace FlightBooking.Core
{
    public class DiscountedPassenger : Passenger
    {
	    public override int AllowedBags => 0;

	    public override double GetPrice(double price, int flightAwardedPoints)
		{
			return price / 2;
		}
    }
}