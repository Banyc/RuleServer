using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using RuleEngine.Models.Expression;

namespace RuleEngine.Helpers
{
    public class SimpleExpressionNodeEqualityComparer : IEqualityComparer<ISimpleExpression>
    {
        public bool Equals(ISimpleExpression x, ISimpleExpression y)
        {
            if (x is SimpleExpressionBinary xBinaryNode && y is SimpleExpressionBinary yBinaryNode)
            {
                return xBinaryNode.Operator == yBinaryNode.Operator;
            }
            if (x is SimpleExpressionParataxis xParataxis && y is SimpleExpressionParataxis yParataxis)
            {
                return xParataxis.Operator == yParataxis.Operator &&
                    xParataxis.Operands.Count == yParataxis.Operands.Count;
            }
            if (x is SimpleExpressionConstant xConstantNode && y is SimpleExpressionConstant yConstantNode)
            {
                if (xConstantNode.Value is string xString && yConstantNode.Value is string yString)
                {
                    return xString == yString;
                }
                if (xConstantNode.Value is int xInt && yConstantNode.Value is int yInt)
                {
                    return xInt == yInt;
                }
                if (xConstantNode.Value is float xFloat && yConstantNode.Value is float yFloat)
                {
                    return xFloat == yFloat;
                }
                if (xConstantNode.Value is double xDouble && yConstantNode.Value is double yDouble)
                {
                    return xDouble == yDouble;
                }
                return false;
            }
            if (x is SimpleExpressionParameter xParameter && y is SimpleExpressionParameter yParameter)
            {
                return xParameter.ParameterName == yParameter.ParameterName;
            }
            if (x is SimpleExpressionUnary xUnaryNode && y is SimpleExpressionUnary yUnaryNode)
            {
                return xUnaryNode.Operator == yUnaryNode.Operator;
            }
            return false;
        }

        public int GetHashCode([DisallowNull] ISimpleExpression obj)
        {
            if (obj is SimpleExpressionBinary binaryNode)
            {
                return binaryNode.Operator.GetHashCode();
            }
            if (obj is SimpleExpressionParataxis parataxisNode)
            {
                return parataxisNode.Operator.GetHashCode() +
                    parataxisNode.Operands.Count.GetHashCode();
            }
            if (obj is SimpleExpressionConstant constantNode)
            {
                if (constantNode.Value is string constantString)
                {
                    return constantString.GetHashCode();
                }
                if (constantNode.Value is int constantInt)
                {
                    return constantInt.GetHashCode();
                }
                if (constantNode.Value is float constantFloat)
                {
                    return constantFloat.GetHashCode();
                }
                if (constantNode.Value is double constantDouble)
                {
                    return constantDouble.GetHashCode();
                }
                throw new System.NotImplementedException();
            }
            if (obj is SimpleExpressionParameter parameter)
            {
                return parameter.ParameterName.GetHashCode();
            }
            if (obj is SimpleExpressionUnary unaryNode)
            {
                return unaryNode.Operator.GetHashCode();
            }
            throw new System.NotImplementedException();
        }
    }
}
