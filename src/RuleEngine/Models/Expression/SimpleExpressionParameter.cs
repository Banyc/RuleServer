using System;
using System.Collections.Generic;

namespace RuleEngine.Models.Expression
{
    public class SimpleExpressionParameter : ISimpleExpression
    {
        public string ParameterName { get; set; }

        public object GetValue(IDictionary<string, object> parameterValuePairs)
        {
            return parameterValuePairs[this.ParameterName];
        }
    }
}
