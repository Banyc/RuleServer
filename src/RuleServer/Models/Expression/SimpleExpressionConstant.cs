using System;
using System.Collections.Generic;

namespace RuleServer.Models.Expression
{
    public class SimpleExpressionConstant : ISimpleExpression
    {
        public object Value { get; set; }

        public object GetValue(IDictionary<string, object> parameterValuePairs)
        {
            return this.Value;
        }
    }
}
