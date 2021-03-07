using System.Collections.Generic;

namespace RuleServer.Models.Expression
{
    public enum SimpleExpressionUnaryOperator
    {
        Plus,
        Minus,
        LogicalNot,
    }

    public class SimpleExpressionUnary : ISimpleExpression
    {
        public ISimpleExpression Operand { get; set; }
        public SimpleExpressionUnaryOperator Operator { get; set; }
        public object GetValue(IDictionary<string, object> parameterValuePairs)
        {
            dynamic operandValue = this.Operand.GetValue(parameterValuePairs);

            switch (this.Operator)
            {
                case SimpleExpressionUnaryOperator.Plus:
                    return +operandValue;
                case SimpleExpressionUnaryOperator.Minus:
                    return -operandValue;
                case SimpleExpressionUnaryOperator.LogicalNot:
                    return !operandValue;
                default:
                    break;
            }

            return null;
        }
    }
}
