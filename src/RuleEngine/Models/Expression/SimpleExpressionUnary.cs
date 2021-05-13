using System.Collections.Generic;

namespace RuleEngine.Models.Expression
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
            return GetValue(parameterValuePairs, null);
        }

        public object GetValue(IDictionary<string, object> parameterValuePairs, object operandValue)
        {
            dynamic operandValueDynamic = operandValue ?? (dynamic)this.Operand.GetValue(parameterValuePairs);

            switch (this.Operator)
            {
                case SimpleExpressionUnaryOperator.Plus:
                    return +operandValueDynamic;
                case SimpleExpressionUnaryOperator.Minus:
                    return -operandValueDynamic;
                case SimpleExpressionUnaryOperator.LogicalNot:
                    return !operandValueDynamic;
                default:
                    break;
            }

            return null;
        }
    }
}
