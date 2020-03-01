namespace FlightBooking.Core
{
    public interface IFlightRoute
    {
        double BasePrice { get; set; }
        double BaseCost { get; set; }
        int LoyaltyPointsGained { get; set; }
        double MinimumTakeOffPercentage { get; set; }
    }

    public class FlightRoute : IFlightRoute
    {
        private readonly string _origin;
        private readonly string _destination;

        public FlightRoute(string origin, string destination)
        {
            _origin = origin;
            _destination = destination;
        }

        public double BasePrice { get; set; }
        public double BaseCost { get; set; }
        public int LoyaltyPointsGained { get; set; }
        public double MinimumTakeOffPercentage { get; set; }
		
		public override string ToString() {
			return string.Format($"{_origin} to {_destination}");
		}
    }
}