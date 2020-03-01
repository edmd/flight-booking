namespace FlightBooking.Core
{
    public class Plane : IPlane
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumberOfSeats { get; set; }
    }
}
