namespace FlightBooking.Core
{
    public abstract class Passenger : IPassenger
    {
        public string Name { get; set; }
		
        public int Age { get; set; }
        
        public virtual int AllowedBags => 1;

        public abstract double GetPrice(double price, int flightAwardedPoints);
    }
}