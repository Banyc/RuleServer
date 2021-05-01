using System.Collections.Generic;
using RuleServer.Models.Expression;

namespace src.RuleServer.Models.Expression
{
    public class SimpleExpressionStub : ISimpleExpression
    {
        public int StubIndex { get; set; }
        public ISimpleExpression ExpressionTree { get; set; }
        public object GetValue(IDictionary<string, object> parameterValuePairs)
        {
            return this.ExpressionTree.GetValue(parameterValuePairs);
        }
        public object GetValue(IDictionary<string, object> parameterValuePairs,
                               object[] computedValues)
        {
            if (computedValues[this.StubIndex] == null)
            {
                var value = this.ExpressionTree.GetValue(parameterValuePairs);
                computedValues[this.StubIndex] = value;
            }
            return computedValues[this.StubIndex];
        }
    }
}
