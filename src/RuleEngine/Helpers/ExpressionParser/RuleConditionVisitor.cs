using Antlr4.Runtime.Misc;
using RuleEngine.Models.Expression;

namespace RuleEngine.Helpers.ExpressionParser
{
    public class RuleConditionVisitor : RuleConditionBaseVisitor<object>
    {
        public override object VisitStart([NotNull] RuleConditionParser.StartContext context)
        {
            return Visit(context.expr());
        }

        public override object VisitAtom([NotNull] RuleConditionParser.AtomContext context)
        {
            if (context.constant() != null)
            {
                return Visit(context.constant());
            }
            else if (context.variable() != null)
            {
                return Visit(context.variable());
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        // public override object VisitCompOp([NotNull] RuleConditionParser.CompOpContext context)
        // {
        //     throw new System.NotImplementedException();
        // }

        public override object VisitConstant([NotNull] RuleConditionParser.ConstantContext context)
        {
            if (context.INT_NUMBER() != null)
            {
                SimpleExpressionConstant constant = new();
                constant.Value = int.Parse(context.INT_NUMBER().GetText());
                return constant;
            }
            else if (context.DECIMAL_NUMBER() != null)
            {
                SimpleExpressionConstant constant = new();
                constant.Value = double.Parse(context.DECIMAL_NUMBER().GetText());
                return constant;
            }
            else if (context.SINGLE_QUOTED_TEXT() != null || context.DOUBLE_QUOTED_TEXT() != null)
            {
                SimpleExpressionConstant constant = new();
                string withQuotes = null;
                if (context.SINGLE_QUOTED_TEXT() != null)
                {
                    withQuotes = context.SINGLE_QUOTED_TEXT().GetText();
                }
                else if (context.DOUBLE_QUOTED_TEXT() != null)
                {
                    withQuotes = context.DOUBLE_QUOTED_TEXT().GetText();
                }
                constant.Value = withQuotes.Substring(1, withQuotes.Length - 2);
                return constant;
            }
            else
            {
                throw new System.NotImplementedException();
            }
        }

        public override object VisitExprAdd([NotNull] RuleConditionParser.ExprAddContext context)
        {
            var expression = GetExpressionBinary(context);
            expression.Operator = SimpleExpressionBinaryOperator.Add;
            return GetSimplifiedExpression(expression);
        }

        public override object VisitExprAnd([NotNull] RuleConditionParser.ExprAndContext context)
        {
            var expression = GetExpressionBinary(context);
            expression.Operator = SimpleExpressionBinaryOperator.And;
            return GetSimplifiedExpression(expression);
        }

        public override object VisitExprAtom([NotNull] RuleConditionParser.ExprAtomContext context)
        {
            return Visit(context.atom());
        }

        public override object VisitExprCompare([NotNull] RuleConditionParser.ExprCompareContext context)
        {
            var expression = GetExpressionBinary(context);

            if (context.compOp().EQUAL_OPERATOR() != null)
                expression.Operator = SimpleExpressionBinaryOperator.Equal;
            else if (context.compOp().GREATER_OR_EQUAL_OPERATOR() != null)
                expression.Operator = SimpleExpressionBinaryOperator.GreaterOrEqual;
            else if (context.compOp().GREATER_THAN_OPERATOR() != null)
                expression.Operator = SimpleExpressionBinaryOperator.Greater;
            else if (context.compOp().LESS_OR_EQUAL_OPERATOR() != null)
                expression.Operator = SimpleExpressionBinaryOperator.LessOrEqual;
            else if (context.compOp().LESS_THAN_OPERATOR() != null)
                expression.Operator = SimpleExpressionBinaryOperator.Less;
            else if (context.compOp().NOT_EQUAL_OPERATOR() != null)
                expression.Operator = SimpleExpressionBinaryOperator.NotEqual;
            else
                throw new System.NotImplementedException();
            // optimize expression
            return GetSimplifiedExpression(expression);
        }

        public override object VisitExprMul([NotNull] RuleConditionParser.ExprMulContext context)
        {
            var expression = GetExpressionBinary(context);
            expression.Operator = SimpleExpressionBinaryOperator.Mul;
            // optimize expression
            return GetSimplifiedExpression(expression);
        }

        public override object VisitExprNot([NotNull] RuleConditionParser.ExprNotContext context)
        {
            // var expression = GetExpressionUnary(context);
            // expression.Operator = SimpleExpressionUnaryOperator.LogicalNot;
            // return GetSimplifiedExpression(expression);
            var childOperand = Visit(context.expr()) as ISimpleExpression;
            NegateBooleanExpression(childOperand);
            return childOperand;
        }

        public override object VisitExprOr([NotNull] RuleConditionParser.ExprOrContext context)
        {
            var expression = GetExpressionBinary(context);
            expression.Operator = SimpleExpressionBinaryOperator.Or;
            // optimize expression
            return GetSimplifiedExpression(expression);
        }

        public override object VisitExprPar([NotNull] RuleConditionParser.ExprParContext context)
        {
            return Visit(context.expr());
        }

        public override object VisitExprSign([NotNull] RuleConditionParser.ExprSignContext context)
        {
            var expression = GetExpressionUnary(context);

            if (context.PLUS_OPERATOR() != null)
            {
                expression.Operator = SimpleExpressionUnaryOperator.Plus;
            }
            else if (context.MINUS_OPERATOR() != null)
            {
                expression.Operator = SimpleExpressionUnaryOperator.Minus;
            }
            return GetSimplifiedExpression(expression);
        }

        // public override object VisitIdentifier([NotNull] RuleConditionParser.IdentifierContext context)
        // {
        //     throw new System.NotImplementedException();
        // }

        public override object VisitVariable([NotNull] RuleConditionParser.VariableContext context)
        {
            SimpleExpressionParameter parameter = new()
            {
                ParameterName = context.identifier().GetText()
            };
            return parameter;
        }

        private SimpleExpressionBinary GetExpressionBinary(dynamic context)
        {
            SimpleExpressionBinary expressionBinary = new();
            expressionBinary.LeftOperand = (ISimpleExpression)Visit(context.expr(0));
            expressionBinary.RightOperand = (ISimpleExpression)Visit(context.expr(1));
            return expressionBinary;
        }
        private SimpleExpressionUnary GetExpressionUnary(dynamic context)
        {
            SimpleExpressionUnary expressionUnary = new();
            expressionUnary.Operand = (ISimpleExpression)Visit(context.expr(0));
            return expressionUnary;
        }
        private static ISimpleExpression GetSimplifiedExpression(SimpleExpressionBinary expressionBinary)
        {
            // optimize expression
            if (expressionBinary.LeftOperand is SimpleExpressionConstant constant
                && expressionBinary.RightOperand is SimpleExpressionConstant)
            {
                constant.Value = expressionBinary.GetValue(null);
                return constant;
            }
            return expressionBinary;
        }
        private static ISimpleExpression GetSimplifiedExpression(SimpleExpressionUnary expressionUnary)
        {
            // optimize expression
            if (expressionUnary.Operand is SimpleExpressionConstant constant)
            {
                constant.Value = expressionUnary.GetValue(null);
                return constant;
            }
            return expressionUnary;
        }

        private void NegateBooleanExpression(ISimpleExpression expression)
        {
            if (expression is SimpleExpressionBinary expressionBinary)
            {
                NegateBooleanExpression(expressionBinary.LeftOperand);
                NegateBooleanExpression(expressionBinary.RightOperand);

                switch (expressionBinary.Operator)
                {
                    case SimpleExpressionBinaryOperator.GreaterOrEqual:
                        expressionBinary.Operator = SimpleExpressionBinaryOperator.LessOrEqual;
                        break;
                    case SimpleExpressionBinaryOperator.Greater:
                        expressionBinary.Operator = SimpleExpressionBinaryOperator.Less;
                        break;
                    case SimpleExpressionBinaryOperator.Equal:
                        expressionBinary.Operator = SimpleExpressionBinaryOperator.NotEqual;
                        break;
                    case SimpleExpressionBinaryOperator.LessOrEqual:
                        expressionBinary.Operator = SimpleExpressionBinaryOperator.GreaterOrEqual;
                        break;
                    case SimpleExpressionBinaryOperator.Less:
                        expressionBinary.Operator = SimpleExpressionBinaryOperator.Greater;
                        break;
                    case SimpleExpressionBinaryOperator.NotEqual:
                        expressionBinary.Operator = SimpleExpressionBinaryOperator.Equal;
                        break;
                    case SimpleExpressionBinaryOperator.And:
                        expressionBinary.Operator = SimpleExpressionBinaryOperator.Or;
                        break;
                    case SimpleExpressionBinaryOperator.Or:
                        expressionBinary.Operator = SimpleExpressionBinaryOperator.And;
                        break;
                    default:
                        throw new System.Exception($"Operator {expressionBinary.Operator} is not a boolean expression");
                }
            }
        }
    }
}
