using System;
using RuleEngine.Models.RuleEngine;

namespace RuleEngine.Exceptions
{
    public class RuleEngineParseException : Exception
    {
        public RuleGroupsParseErrorArgs Details { get; set; }

        public RuleEngineParseException() : base()
        {
        }

        public RuleEngineParseException(string message) : base(message)
        {
        }

        public RuleEngineParseException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
