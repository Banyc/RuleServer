//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.9.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from RuleCondition.g4 by ANTLR 4.9.1

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete generic visitor for a parse tree produced
/// by <see cref="RuleConditionParser"/>.
/// </summary>
/// <typeparam name="Result">The return type of the visit operation.</typeparam>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.1")]
[System.CLSCompliant(false)]
public interface IRuleConditionVisitor<Result> : IParseTreeVisitor<Result> {
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleConditionParser.start"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitStart([NotNull] RuleConditionParser.StartContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprAtom</c>
	/// labeled alternative in <see cref="RuleConditionParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprAtom([NotNull] RuleConditionParser.ExprAtomContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprOr</c>
	/// labeled alternative in <see cref="RuleConditionParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprOr([NotNull] RuleConditionParser.ExprOrContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprNot</c>
	/// labeled alternative in <see cref="RuleConditionParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprNot([NotNull] RuleConditionParser.ExprNotContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprMul</c>
	/// labeled alternative in <see cref="RuleConditionParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprMul([NotNull] RuleConditionParser.ExprMulContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprPar</c>
	/// labeled alternative in <see cref="RuleConditionParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprPar([NotNull] RuleConditionParser.ExprParContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprAdd</c>
	/// labeled alternative in <see cref="RuleConditionParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprAdd([NotNull] RuleConditionParser.ExprAddContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprAnd</c>
	/// labeled alternative in <see cref="RuleConditionParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprAnd([NotNull] RuleConditionParser.ExprAndContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprSign</c>
	/// labeled alternative in <see cref="RuleConditionParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprSign([NotNull] RuleConditionParser.ExprSignContext context);
	/// <summary>
	/// Visit a parse tree produced by the <c>exprCompare</c>
	/// labeled alternative in <see cref="RuleConditionParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitExprCompare([NotNull] RuleConditionParser.ExprCompareContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleConditionParser.compOp"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitCompOp([NotNull] RuleConditionParser.CompOpContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleConditionParser.atom"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitAtom([NotNull] RuleConditionParser.AtomContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleConditionParser.constant"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitConstant([NotNull] RuleConditionParser.ConstantContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleConditionParser.variable"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitVariable([NotNull] RuleConditionParser.VariableContext context);
	/// <summary>
	/// Visit a parse tree produced by <see cref="RuleConditionParser.identifier"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	/// <return>The visitor result.</return>
	Result VisitIdentifier([NotNull] RuleConditionParser.IdentifierContext context);
}
