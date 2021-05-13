using System.Collections.Generic;

namespace RuleEngine.Models.Expression
{
    public class SimpleExpressionParataxis : ISimpleExpression
    {
        public SimpleExpressionBinaryOperator Operator { get; set; }
        public List<ISimpleExpression> Operands { get; set; } = new();

        public object GetValue(IDictionary<string, object> parameterValuePairs)
        {
            return GetValue(parameterValuePairs, null);
        }

        public object GetValue(IDictionary<string, object> parameterValuePairs,
                               List<object> operandValues)
        {
            if (operandValues == null)
            {
                bool isFirstResult = true;
                dynamic result = null;
                foreach (var operand in this.Operands)
                {
                    dynamic operandValue = operand.GetValue(parameterValuePairs);

                    if (isFirstResult)
                    {
                        result = operandValue;
                    }
                    else
                    {
                        result = SimpleExpressionBinary.GetValue(this.Operator, result, operandValue);
                    }
                    isFirstResult = false;
                }
                return result;
            }
            else
            {
                bool isFirstResult = true;
                dynamic result = null;
                foreach (var operandValue in operandValues)
                {
                    if (isFirstResult)
                    {
                        result = operandValue;
                    }
                    else
                    {
                        result = SimpleExpressionBinary.GetValue(this.Operator, result, operandValue);
                    }
                    isFirstResult = false;
                }
                return result;
            }
        }

        // no min-term optimization
        // prefer `optimizedChild` to `originalChild`
        private void AddChild(SimpleExpressionParataxis optimizedChild, ISimpleExpression originalChild)
        {
            if (optimizedChild == null)
            {
                this.AddChild(originalChild);
            }
            else
            {
                this.AddChild(optimizedChild);
            }
        }

        // no min-term optimization
        private void AddChild(ISimpleExpression child)
        {
            if (child == null)
            {
                throw new System.Exception("Child cannot be null");
                return;
            }
            if (child is SimpleExpressionBinary binaryChild)
            {
                if (binaryChild.Operator == this.Operator)
                {
                    // flatten the tree structure

                    // add left child
                    SimpleExpressionParataxis leftTempChild = new()
                    {
                        Operator = binaryChild.Operator
                    };
                    leftTempChild.AddChild(binaryChild.LeftOperand);
                    this.Operands.AddRange(leftTempChild.Operands);

                    // add right child
                    SimpleExpressionParataxis rightTempChild = new()
                    {
                        Operator = binaryChild.Operator
                    };
                    rightTempChild.AddChild(binaryChild.RightOperand);
                    this.Operands.AddRange(rightTempChild.Operands);
                }
                else
                {
                    this.Operands.Add(binaryChild);
                }
            }
            else if (child is SimpleExpressionParataxis parataxisChild)
            {
                if (parataxisChild.Operator == this.Operator)
                {
                    this.Operands.AddRange(parataxisChild.Operands);
                }
                else
                {
                    this.Operands.Add(parataxisChild);
                }
            }
            else
            {
                this.Operands.Add(child);
            }
        }

        // when return `and` expression, there should be NO immediate `or` child expression
        // with min-term optimization
        // return null when the operator is not `and` or `or`
        public static SimpleExpressionParataxis GetMinTerms(SimpleExpressionBinary root)
        {
            SimpleExpressionParataxis newRoot = new();

            SimpleExpressionParataxis leftParataxis = null;
            SimpleExpressionParataxis rightParataxis = null;
            if (root.LeftOperand is SimpleExpressionBinary leftBinary)
            {
                leftParataxis = GetMinTerms(leftBinary);
            }
            if (root.RightOperand is SimpleExpressionBinary rightBinary)
            {
                rightParataxis = GetMinTerms(rightBinary);
            }
            if (root.Operator == SimpleExpressionBinaryOperator.Or)
            {
                newRoot.Operator = SimpleExpressionBinaryOperator.Or;

                newRoot.AddChild(optimizedChild: leftParataxis, originalChild: root.LeftOperand);
                newRoot.AddChild(optimizedChild: rightParataxis, originalChild: root.RightOperand);
                return newRoot;
            }
            if (root.Operator == SimpleExpressionBinaryOperator.And)
            {
                if (leftParataxis?.Operator != SimpleExpressionBinaryOperator.Or &&
                    rightParataxis?.Operator != SimpleExpressionBinaryOperator.Or)
                {
                    newRoot.Operator = SimpleExpressionBinaryOperator.And;

                    newRoot.AddChild(optimizedChild: leftParataxis, originalChild: root.LeftOperand);
                    newRoot.AddChild(optimizedChild: rightParataxis, originalChild: root.RightOperand);
                    return newRoot;
                }
                if (leftParataxis?.Operator == SimpleExpressionBinaryOperator.Or &&
                    rightParataxis?.Operator == SimpleExpressionBinaryOperator.Or)
                {
                    newRoot.Operator = SimpleExpressionBinaryOperator.Or;

                    foreach (var leftGrandChild in leftParataxis.Operands)
                    {
                        foreach (var rightGrandChild in rightParataxis.Operands)
                        {
                            SimpleExpressionParataxis newChild = new()
                            {
                                Operator = SimpleExpressionBinaryOperator.And
                            };
                            newChild.AddChild(leftGrandChild);
                            newChild.AddChild(rightGrandChild);

                            newRoot.Operands.Add(newChild);
                        }
                    }
                    return newRoot;
                }
                if ((
                        leftParataxis?.Operator == SimpleExpressionBinaryOperator.Or &&
                        rightParataxis == null)
                    ||
                    (
                        rightParataxis?.Operator == SimpleExpressionBinaryOperator.Or &&
                        leftParataxis == null))
                {
                    newRoot.Operator = SimpleExpressionBinaryOperator.Or;

                    SimpleExpressionParataxis childParataxis;
                    ISimpleExpression childNotParataxis;
                    if (rightParataxis != null)
                    {
                        childParataxis = rightParataxis;
                        childNotParataxis = root.LeftOperand;
                    }
                    else
                    {
                        childParataxis = leftParataxis;
                        childNotParataxis = root.RightOperand;
                    }

                    foreach (var grandChild in childParataxis.Operands)
                    {
                        SimpleExpressionParataxis newChild = new()
                        {
                            Operator = SimpleExpressionBinaryOperator.And
                        };
                        newChild.AddChild(grandChild);
                        newChild.AddChild(childNotParataxis);

                        newRoot.Operands.Add(newChild);
                    }
                    return newRoot;
                }
                // not possible to be here
                throw new System.NotImplementedException();
            }
            // only flatten `and` and `or` operators
            return null;
        }
    }
}
