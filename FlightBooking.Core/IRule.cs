namespace FlightBooking.Core
{
    // Expansion for Rules Engine 
    public interface IRule
    {
        string RuleName { get; set; }
        bool RuleEnabled { get; set; }
        int RulePriority { get; set; }
        RuleType RuleType { get; set; }
        string ResponseCode { get; set; }
        string ResponseMessage { get; set; } 
    }

    public enum RuleType
    {
        Profitable,
        Capacity,
        AirlineStaffOverride,
        MinimumOccupancy
    }
}