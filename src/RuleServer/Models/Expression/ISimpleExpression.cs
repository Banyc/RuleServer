using System;
using System.Collections.Generic;
namespace RuleServer.Models.Expression
{
    public interface ISimpleExpression
    {
        object GetValue(IDictionary<string, object> parameterValuePairs);
    }
}
