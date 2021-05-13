using System;
using System.Collections.Generic;
namespace RuleEngine.Models.Expression
{
    public interface ISimpleExpression
    {
        object GetValue(IDictionary<string, object> parameterValuePairs);
    }
}
