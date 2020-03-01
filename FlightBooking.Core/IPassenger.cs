namespace FlightBooking.Core
{
    public interface IPassenger
    {
        string Name { get; set; }
        int Age { get; set; }
        int AllowedBags { get; }
        
        double GetPrice(double price, int flightAwardedPoints);
    }
}