namespace FlightBooking.Core
{
    public interface IPlane
    {
        int Id { get; set; }
        string Name { get; set; }
        int NumberOfSeats { get; set; }
    }
}