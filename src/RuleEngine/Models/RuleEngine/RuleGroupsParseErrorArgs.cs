using System.Text;
using System.Collections.Generic;

namespace RuleEngine.Models.RuleEngine
{
    public class RuleParseErrorArgs
    {
        public string RuleName { get; set; }
        public int ListIndex { get; set; }
        public string Message { get; set; }
        public int Line { get; set; }
        public int CharPositionInLine { get; set; }

        public override string ToString()
        {
            return $"{RuleName}: {Line}:{CharPositionInLine} {Message}.";
        }
    }

    public class RuleGroupParseErrorArgs
    {
        public string GroupName { get; set; }
        public List<RuleParseErrorArgs> Rules { get; set; } = new();

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.Append('[').Append(GroupName).AppendLine("]");
            foreach (var rule in Rules)
            {
                stringBuilder.AppendLine(rule.ToString());
            }
            return stringBuilder.ToString();
        }
    }

    public class RuleGroupsParseErrorArgs
    {
        public List<RuleGroupParseErrorArgs> RuleGroups { get; set; } = new();

        public override string ToString()
        {
            StringBuilder stringBuilder = new();
            stringBuilder.AppendLine("[[Errors]]");
            foreach (var group in RuleGroups)
            {
                stringBuilder.AppendLine(group.ToString());
            }
            return stringBuilder.ToString();
        }
    }
}
