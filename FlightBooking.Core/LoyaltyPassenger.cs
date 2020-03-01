using System;

namespace FlightBooking.Core
{
    public class LoyaltyPassenger : Passenger, IPassenger
    {
	    public int TotalPoints { get; set; }
	    
	    public bool UsePoints { get; set; }

	    public override int AllowedBags => 2;
	    
	    public override double GetPrice(double price, int flightAwardedPoints) {
			if (UsePoints)
			{
				if (price - TotalPoints <= 0) // enough points
				{
					TotalPoints -= Convert.ToInt32(price);
					return 0;
				}
				else // not enough points, revert to using no points
				{
					TotalPoints += flightAwardedPoints;
					UsePoints = false;
					return price;
				}
			}
			else
			{
				TotalPoints += flightAwardedPoints;
				return price;
			}
		}
    }
}