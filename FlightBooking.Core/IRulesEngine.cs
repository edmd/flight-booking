namespace FlightBooking.Core
{
    public interface IRulesEngine
    {
        bool ApplyRules(ScheduledFlight scheduledFlight);
    }
}