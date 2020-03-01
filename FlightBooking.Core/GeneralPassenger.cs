namespace FlightBooking.Core
{
    public class GeneralPassenger : Passenger
    {
        public override double GetPrice(double price, int flightAwardedPoints)
        {
            return price;
        }
    }
}