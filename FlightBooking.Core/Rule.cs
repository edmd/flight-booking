namespace FlightBooking.Core
{
    public class Rule : IRule
    {
        public string RuleName { get; set; }
        public bool RuleEnabled { get; set; }
        public int RulePriority { get; set; }
        public RuleType RuleType { get; set; }
        public string ResponseCode { get; set; }
        public string ResponseMessage { get; set; }
    }
}