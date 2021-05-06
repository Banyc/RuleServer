using System;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace RuleServer.Models.Expression
{
    public enum SimpleExpressionBinaryOperator
    {
        Mul,
        Div,
        Add,
        Sub,
        GreaterOrEqual,
        Greater,
        Equal,
        LessOrEqual,
        Less,
        NotEqual,
        And,
        Or,
        // Xor,
    }

    public class SimpleExpressionBinary : ISimpleExpression
    {
        public ISimpleExpression LeftOperand { get; set; }
        public ISimpleExpression RightOperand { get; set; }
        public SimpleExpressionBinaryOperator Operator { get; set; }

        public object GetValue(IDictionary<string, object> parameterValuePairs)
        {
            return GetValue(parameterValuePairs, null, null);
        }

        public object GetValue(IDictionary<string, object> parameterValuePairs,
                               object leftOperandValue,
                               object rightOperandValue)
        {
            dynamic leftOperandValueDynamic = leftOperandValue ?? (dynamic)this.LeftOperand.GetValue(parameterValuePairs);
            switch (this.Operator)
            {
                case SimpleExpressionBinaryOperator.And:
                    if (leftOperandValueDynamic == false)
                    {
                        return false;
                    }
                    break;
                case SimpleExpressionBinaryOperator.Or:
                    if (leftOperandValueDynamic == true)
                    {
                        return true;
                    }
                    break;
            }

            dynamic rightOperandValueDynamic = rightOperandValue ?? (dynamic)this.RightOperand.GetValue(parameterValuePairs);

            switch (this.Operator)
            {
                case SimpleExpressionBinaryOperator.Add:
                    return leftOperandValueDynamic + rightOperandValueDynamic;
                case SimpleExpressionBinaryOperator.Mul:
                    return leftOperandValueDynamic * rightOperandValueDynamic;
                case SimpleExpressionBinaryOperator.Div:
                    return leftOperandValueDynamic / rightOperandValueDynamic;
                case SimpleExpressionBinaryOperator.Sub:
                    return leftOperandValueDynamic - rightOperandValueDynamic;
                case SimpleExpressionBinaryOperator.GreaterOrEqual:
                    return leftOperandValueDynamic >= rightOperandValueDynamic;
                case SimpleExpressionBinaryOperator.Greater:
                    return leftOperandValueDynamic > rightOperandValueDynamic;
                case SimpleExpressionBinaryOperator.Equal:
                    return leftOperandValueDynamic == rightOperandValueDynamic;
                case SimpleExpressionBinaryOperator.LessOrEqual:
                    return leftOperandValueDynamic <= rightOperandValueDynamic;
                case SimpleExpressionBinaryOperator.Less:
                    return leftOperandValueDynamic < rightOperandValueDynamic;
                case SimpleExpressionBinaryOperator.NotEqual:
                    return leftOperandValueDynamic != rightOperandValueDynamic;
                case SimpleExpressionBinaryOperator.And:
                    return leftOperandValueDynamic && rightOperandValueDynamic;
                case SimpleExpressionBinaryOperator.Or:
                    return leftOperandValueDynamic || rightOperandValueDynamic;
                // case SimpleExpressionBinaryOperator.Xor:
                //     return leftOperandValueDynamic ^ rightOperandValueDynamic;
                default:
                    break;
            }

            return null;
        }
    }
}
