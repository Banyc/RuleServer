using System;

namespace RuleEngine.Exceptions
{
    public class ParseErrorException : Exception
    {
        public int Line { get; set; }
        public int CharPositionInLine { get; set; }
        public ParseErrorException() : base() { }
        public ParseErrorException(string message) : base(message) { }
        public ParseErrorException(string message, Exception innerException) : base(message, innerException) { }
    }
}
