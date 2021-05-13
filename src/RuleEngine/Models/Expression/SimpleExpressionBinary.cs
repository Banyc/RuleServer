using System;
using System.Collections.Generic;
using System.Reflection.Emit;
namespace RuleEngine.Models.Expression
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

            return SimpleExpressionBinary.GetValue(this.Operator, leftOperandValueDynamic, rightOperandValueDynamic);
        }

        public static object GetValue(SimpleExpressionBinaryOperator binaryOperator, dynamic leftOperandValue, dynamic rightOperandValue)
        {
            switch (binaryOperator)
            {
                case SimpleExpressionBinaryOperator.Add:
                    return leftOperandValue + rightOperandValue;
                case SimpleExpressionBinaryOperator.Mul:
                    return leftOperandValue * rightOperandValue;
                case SimpleExpressionBinaryOperator.Div:
                    return leftOperandValue / rightOperandValue;
                case SimpleExpressionBinaryOperator.Sub:
                    return leftOperandValue - rightOperandValue;
                case SimpleExpressionBinaryOperator.GreaterOrEqual:
                    return leftOperandValue >= rightOperandValue;
                case SimpleExpressionBinaryOperator.Greater:
                    return leftOperandValue > rightOperandValue;
                case SimpleExpressionBinaryOperator.Equal:
                    return leftOperandValue == rightOperandValue;
                case SimpleExpressionBinaryOperator.LessOrEqual:
                    return leftOperandValue <= rightOperandValue;
                case SimpleExpressionBinaryOperator.Less:
                    return leftOperandValue < rightOperandValue;
                case SimpleExpressionBinaryOperator.NotEqual:
                    return leftOperandValue != rightOperandValue;
                case SimpleExpressionBinaryOperator.And:
                    return leftOperandValue && rightOperandValue;
                case SimpleExpressionBinaryOperator.Or:
                    return leftOperandValue || rightOperandValue;
                // case SimpleExpressionBinaryOperator.Xor:
                //     return leftOperandValue ^ rightOperandValue;
                default:
                    break;
            }

            return null;
        }
    }
}
