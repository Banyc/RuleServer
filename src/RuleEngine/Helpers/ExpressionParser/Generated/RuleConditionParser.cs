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

using System;
using System.IO;
using System.Text;
using System.Diagnostics;
using System.Collections.Generic;
using Antlr4.Runtime;
using Antlr4.Runtime.Atn;
using Antlr4.Runtime.Misc;
using Antlr4.Runtime.Tree;
using DFA = Antlr4.Runtime.Dfa.DFA;

[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.9.1")]
[System.CLSCompliant(false)]
public partial class RuleConditionParser : Parser {
	protected static DFA[] decisionToDFA;
	protected static PredictionContextCache sharedContextCache = new PredictionContextCache();
	public const int
		WhiteSpace=1, HEX_NUMBER=2, BIN_NUMBER=3, INT_NUMBER=4, DECIMAL_NUMBER=5, 
		FLOAT_NUMBER=6, EQUAL_OPERATOR=7, GREATER_OR_EQUAL_OPERATOR=8, GREATER_THAN_OPERATOR=9, 
		LESS_OR_EQUAL_OPERATOR=10, LESS_THAN_OPERATOR=11, NOT_EQUAL_OPERATOR=12, 
		PLUS_OPERATOR=13, MINUS_OPERATOR=14, MULT_OPERATOR=15, DIV_OPERATOR=16, 
		MOD_OPERATOR=17, LOGICAL_NOT_OPERATOR=18, BITWISE_NOT_OPERATOR=19, SHIFT_LEFT_OPERATOR=20, 
		SHIFT_RIGHT_OPERATOR=21, LOGICAL_AND_OPERATOR=22, BITWISE_AND_OPERATOR=23, 
		BITWISE_XOR_OPERATOR=24, LOGICAL_OR_OPERATOR=25, BITWISE_OR_OPERATOR=26, 
		DOT_SYMBOL=27, COMMA_SYMBOL=28, SEMICOLON_SYMBOL=29, COLON_SYMBOL=30, 
		OPEN_PAR_SYMBOL=31, CLOSE_PAR_SYMBOL=32, OPEN_CURLY_SYMBOL=33, CLOSE_CURLY_SYMBOL=34, 
		UNDERLINE_SYMBOL=35, IDENTIFIER=36, DOUBLE_QUOTED_TEXT=37, SINGLE_QUOTED_TEXT=38, 
		EQUAL2_OPERATOR=39, NOT_EQUAL2_OPERATOR=40;
	public const int
		RULE_start = 0, RULE_expr = 1, RULE_compOp = 2, RULE_atom = 3, RULE_constant = 4, 
		RULE_variable = 5, RULE_identifier = 6;
	public static readonly string[] ruleNames = {
		"start", "expr", "compOp", "atom", "constant", "variable", "identifier"
	};

	private static readonly string[] _LiteralNames = {
		null, null, null, null, null, null, null, "'='", "'>='", "'>'", "'<='", 
		"'<'", "'!='", "'+'", "'-'", "'*'", "'/'", "'%'", "'!'", "'~'", "'<<'", 
		"'>>'", "'&&'", "'&'", "'^'", "'||'", "'|'", "'.'", "','", "';'", "':'", 
		"'('", "')'", "'{'", "'}'", "'_'", null, null, null, "'=='", "'<>'"
	};
	private static readonly string[] _SymbolicNames = {
		null, "WhiteSpace", "HEX_NUMBER", "BIN_NUMBER", "INT_NUMBER", "DECIMAL_NUMBER", 
		"FLOAT_NUMBER", "EQUAL_OPERATOR", "GREATER_OR_EQUAL_OPERATOR", "GREATER_THAN_OPERATOR", 
		"LESS_OR_EQUAL_OPERATOR", "LESS_THAN_OPERATOR", "NOT_EQUAL_OPERATOR", 
		"PLUS_OPERATOR", "MINUS_OPERATOR", "MULT_OPERATOR", "DIV_OPERATOR", "MOD_OPERATOR", 
		"LOGICAL_NOT_OPERATOR", "BITWISE_NOT_OPERATOR", "SHIFT_LEFT_OPERATOR", 
		"SHIFT_RIGHT_OPERATOR", "LOGICAL_AND_OPERATOR", "BITWISE_AND_OPERATOR", 
		"BITWISE_XOR_OPERATOR", "LOGICAL_OR_OPERATOR", "BITWISE_OR_OPERATOR", 
		"DOT_SYMBOL", "COMMA_SYMBOL", "SEMICOLON_SYMBOL", "COLON_SYMBOL", "OPEN_PAR_SYMBOL", 
		"CLOSE_PAR_SYMBOL", "OPEN_CURLY_SYMBOL", "CLOSE_CURLY_SYMBOL", "UNDERLINE_SYMBOL", 
		"IDENTIFIER", "DOUBLE_QUOTED_TEXT", "SINGLE_QUOTED_TEXT", "EQUAL2_OPERATOR", 
		"NOT_EQUAL2_OPERATOR"
	};
	public static readonly IVocabulary DefaultVocabulary = new Vocabulary(_LiteralNames, _SymbolicNames);

	[NotNull]
	public override IVocabulary Vocabulary
	{
		get
		{
			return DefaultVocabulary;
		}
	}

	public override string GrammarFileName { get { return "RuleCondition.g4"; } }

	public override string[] RuleNames { get { return ruleNames; } }

	public override string SerializedAtn { get { return new string(_serializedATN); } }

	static RuleConditionParser() {
		decisionToDFA = new DFA[_ATN.NumberOfDecisions];
		for (int i = 0; i < _ATN.NumberOfDecisions; i++) {
			decisionToDFA[i] = new DFA(_ATN.GetDecisionState(i), i);
		}
	}

		public RuleConditionParser(ITokenStream input) : this(input, Console.Out, Console.Error) { }

		public RuleConditionParser(ITokenStream input, TextWriter output, TextWriter errorOutput)
		: base(input, output, errorOutput)
	{
		Interpreter = new ParserATNSimulator(this, _ATN, decisionToDFA, sharedContextCache);
	}

	public partial class StartContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public StartContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_start; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterStart(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitStart(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitStart(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public StartContext start() {
		StartContext _localctx = new StartContext(Context, State);
		EnterRule(_localctx, 0, RULE_start);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 14;
			expr(0);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ExprContext : ParserRuleContext {
		public ExprContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_expr; } }
	 
		public ExprContext() { }
		public virtual void CopyFrom(ExprContext context) {
			base.CopyFrom(context);
		}
	}
	public partial class ExprAtomContext : ExprContext {
		[System.Diagnostics.DebuggerNonUserCode] public AtomContext atom() {
			return GetRuleContext<AtomContext>(0);
		}
		public ExprAtomContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterExprAtom(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitExprAtom(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExprAtom(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ExprOrContext : ExprContext {
		public IToken op;
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode LOGICAL_OR_OPERATOR() { return GetToken(RuleConditionParser.LOGICAL_OR_OPERATOR, 0); }
		public ExprOrContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterExprOr(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitExprOr(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExprOr(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ExprNotContext : ExprContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode LOGICAL_NOT_OPERATOR() { return GetToken(RuleConditionParser.LOGICAL_NOT_OPERATOR, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		public ExprNotContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterExprNot(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitExprNot(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExprNot(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ExprMulContext : ExprContext {
		public IToken op;
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode MULT_OPERATOR() { return GetToken(RuleConditionParser.MULT_OPERATOR, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode DIV_OPERATOR() { return GetToken(RuleConditionParser.DIV_OPERATOR, 0); }
		public ExprMulContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterExprMul(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitExprMul(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExprMul(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ExprParContext : ExprContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode OPEN_PAR_SYMBOL() { return GetToken(RuleConditionParser.OPEN_PAR_SYMBOL, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode CLOSE_PAR_SYMBOL() { return GetToken(RuleConditionParser.CLOSE_PAR_SYMBOL, 0); }
		public ExprParContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterExprPar(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitExprPar(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExprPar(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ExprAddContext : ExprContext {
		public IToken op;
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode PLUS_OPERATOR() { return GetToken(RuleConditionParser.PLUS_OPERATOR, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode MINUS_OPERATOR() { return GetToken(RuleConditionParser.MINUS_OPERATOR, 0); }
		public ExprAddContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterExprAdd(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitExprAdd(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExprAdd(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ExprAndContext : ExprContext {
		public IToken op;
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode LOGICAL_AND_OPERATOR() { return GetToken(RuleConditionParser.LOGICAL_AND_OPERATOR, 0); }
		public ExprAndContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterExprAnd(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitExprAnd(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExprAnd(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ExprSignContext : ExprContext {
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr() {
			return GetRuleContext<ExprContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode PLUS_OPERATOR() { return GetToken(RuleConditionParser.PLUS_OPERATOR, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode MINUS_OPERATOR() { return GetToken(RuleConditionParser.MINUS_OPERATOR, 0); }
		public ExprSignContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterExprSign(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitExprSign(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExprSign(this);
			else return visitor.VisitChildren(this);
		}
	}
	public partial class ExprCompareContext : ExprContext {
		public CompOpContext op;
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext[] expr() {
			return GetRuleContexts<ExprContext>();
		}
		[System.Diagnostics.DebuggerNonUserCode] public ExprContext expr(int i) {
			return GetRuleContext<ExprContext>(i);
		}
		[System.Diagnostics.DebuggerNonUserCode] public CompOpContext compOp() {
			return GetRuleContext<CompOpContext>(0);
		}
		public ExprCompareContext(ExprContext context) { CopyFrom(context); }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterExprCompare(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitExprCompare(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitExprCompare(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ExprContext expr() {
		return expr(0);
	}

	private ExprContext expr(int _p) {
		ParserRuleContext _parentctx = Context;
		int _parentState = State;
		ExprContext _localctx = new ExprContext(Context, _parentState);
		ExprContext _prevctx = _localctx;
		int _startState = 2;
		EnterRecursionRule(_localctx, 2, RULE_expr, _p);
		int _la;
		try {
			int _alt;
			EnterOuterAlt(_localctx, 1);
			{
			State = 26;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case OPEN_PAR_SYMBOL:
				{
				_localctx = new ExprParContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;

				State = 17;
				Match(OPEN_PAR_SYMBOL);
				State = 18;
				expr(0);
				State = 19;
				Match(CLOSE_PAR_SYMBOL);
				}
				break;
			case PLUS_OPERATOR:
			case MINUS_OPERATOR:
				{
				_localctx = new ExprSignContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 21;
				_la = TokenStream.LA(1);
				if ( !(_la==PLUS_OPERATOR || _la==MINUS_OPERATOR) ) {
				ErrorHandler.RecoverInline(this);
				}
				else {
					ErrorHandler.ReportMatch(this);
				    Consume();
				}
				State = 22;
				expr(8);
				}
				break;
			case LOGICAL_NOT_OPERATOR:
				{
				_localctx = new ExprNotContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 23;
				Match(LOGICAL_NOT_OPERATOR);
				State = 24;
				expr(7);
				}
				break;
			case INT_NUMBER:
			case DECIMAL_NUMBER:
			case IDENTIFIER:
			case DOUBLE_QUOTED_TEXT:
			case SINGLE_QUOTED_TEXT:
				{
				_localctx = new ExprAtomContext(_localctx);
				Context = _localctx;
				_prevctx = _localctx;
				State = 25;
				atom();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
			Context.Stop = TokenStream.LT(-1);
			State = 46;
			ErrorHandler.Sync(this);
			_alt = Interpreter.AdaptivePredict(TokenStream,2,Context);
			while ( _alt!=2 && _alt!=global::Antlr4.Runtime.Atn.ATN.INVALID_ALT_NUMBER ) {
				if ( _alt==1 ) {
					if ( ParseListeners!=null )
						TriggerExitRuleEvent();
					_prevctx = _localctx;
					{
					State = 44;
					ErrorHandler.Sync(this);
					switch ( Interpreter.AdaptivePredict(TokenStream,1,Context) ) {
					case 1:
						{
						_localctx = new ExprMulContext(new ExprContext(_parentctx, _parentState));
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 28;
						if (!(Precpred(Context, 6))) throw new FailedPredicateException(this, "Precpred(Context, 6)");
						State = 29;
						((ExprMulContext)_localctx).op = TokenStream.LT(1);
						_la = TokenStream.LA(1);
						if ( !(_la==MULT_OPERATOR || _la==DIV_OPERATOR) ) {
							((ExprMulContext)_localctx).op = ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 30;
						expr(7);
						}
						break;
					case 2:
						{
						_localctx = new ExprAddContext(new ExprContext(_parentctx, _parentState));
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 31;
						if (!(Precpred(Context, 5))) throw new FailedPredicateException(this, "Precpred(Context, 5)");
						State = 32;
						((ExprAddContext)_localctx).op = TokenStream.LT(1);
						_la = TokenStream.LA(1);
						if ( !(_la==PLUS_OPERATOR || _la==MINUS_OPERATOR) ) {
							((ExprAddContext)_localctx).op = ErrorHandler.RecoverInline(this);
						}
						else {
							ErrorHandler.ReportMatch(this);
						    Consume();
						}
						State = 33;
						expr(6);
						}
						break;
					case 3:
						{
						_localctx = new ExprCompareContext(new ExprContext(_parentctx, _parentState));
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 34;
						if (!(Precpred(Context, 4))) throw new FailedPredicateException(this, "Precpred(Context, 4)");
						State = 35;
						((ExprCompareContext)_localctx).op = compOp();
						State = 36;
						expr(5);
						}
						break;
					case 4:
						{
						_localctx = new ExprAndContext(new ExprContext(_parentctx, _parentState));
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 38;
						if (!(Precpred(Context, 3))) throw new FailedPredicateException(this, "Precpred(Context, 3)");
						State = 39;
						((ExprAndContext)_localctx).op = Match(LOGICAL_AND_OPERATOR);
						State = 40;
						expr(4);
						}
						break;
					case 5:
						{
						_localctx = new ExprOrContext(new ExprContext(_parentctx, _parentState));
						PushNewRecursionContext(_localctx, _startState, RULE_expr);
						State = 41;
						if (!(Precpred(Context, 2))) throw new FailedPredicateException(this, "Precpred(Context, 2)");
						State = 42;
						((ExprOrContext)_localctx).op = Match(LOGICAL_OR_OPERATOR);
						State = 43;
						expr(3);
						}
						break;
					}
					} 
				}
				State = 48;
				ErrorHandler.Sync(this);
				_alt = Interpreter.AdaptivePredict(TokenStream,2,Context);
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			UnrollRecursionContexts(_parentctx);
		}
		return _localctx;
	}

	public partial class CompOpContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode EQUAL_OPERATOR() { return GetToken(RuleConditionParser.EQUAL_OPERATOR, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode GREATER_OR_EQUAL_OPERATOR() { return GetToken(RuleConditionParser.GREATER_OR_EQUAL_OPERATOR, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode GREATER_THAN_OPERATOR() { return GetToken(RuleConditionParser.GREATER_THAN_OPERATOR, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode LESS_OR_EQUAL_OPERATOR() { return GetToken(RuleConditionParser.LESS_OR_EQUAL_OPERATOR, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode LESS_THAN_OPERATOR() { return GetToken(RuleConditionParser.LESS_THAN_OPERATOR, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode NOT_EQUAL_OPERATOR() { return GetToken(RuleConditionParser.NOT_EQUAL_OPERATOR, 0); }
		public CompOpContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_compOp; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterCompOp(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitCompOp(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitCompOp(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public CompOpContext compOp() {
		CompOpContext _localctx = new CompOpContext(Context, State);
		EnterRule(_localctx, 4, RULE_compOp);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 49;
			_la = TokenStream.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << EQUAL_OPERATOR) | (1L << GREATER_OR_EQUAL_OPERATOR) | (1L << GREATER_THAN_OPERATOR) | (1L << LESS_OR_EQUAL_OPERATOR) | (1L << LESS_THAN_OPERATOR) | (1L << NOT_EQUAL_OPERATOR))) != 0)) ) {
			ErrorHandler.RecoverInline(this);
			}
			else {
				ErrorHandler.ReportMatch(this);
			    Consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class AtomContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ConstantContext constant() {
			return GetRuleContext<ConstantContext>(0);
		}
		[System.Diagnostics.DebuggerNonUserCode] public VariableContext variable() {
			return GetRuleContext<VariableContext>(0);
		}
		public AtomContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_atom; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterAtom(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitAtom(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitAtom(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public AtomContext atom() {
		AtomContext _localctx = new AtomContext(Context, State);
		EnterRule(_localctx, 6, RULE_atom);
		try {
			State = 53;
			ErrorHandler.Sync(this);
			switch (TokenStream.LA(1)) {
			case INT_NUMBER:
			case DECIMAL_NUMBER:
			case DOUBLE_QUOTED_TEXT:
			case SINGLE_QUOTED_TEXT:
				EnterOuterAlt(_localctx, 1);
				{
				State = 51;
				constant();
				}
				break;
			case IDENTIFIER:
				EnterOuterAlt(_localctx, 2);
				{
				State = 52;
				variable();
				}
				break;
			default:
				throw new NoViableAltException(this);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class ConstantContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode INT_NUMBER() { return GetToken(RuleConditionParser.INT_NUMBER, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode DECIMAL_NUMBER() { return GetToken(RuleConditionParser.DECIMAL_NUMBER, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode SINGLE_QUOTED_TEXT() { return GetToken(RuleConditionParser.SINGLE_QUOTED_TEXT, 0); }
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode DOUBLE_QUOTED_TEXT() { return GetToken(RuleConditionParser.DOUBLE_QUOTED_TEXT, 0); }
		public ConstantContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_constant; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterConstant(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitConstant(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitConstant(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public ConstantContext constant() {
		ConstantContext _localctx = new ConstantContext(Context, State);
		EnterRule(_localctx, 8, RULE_constant);
		int _la;
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 55;
			_la = TokenStream.LA(1);
			if ( !((((_la) & ~0x3f) == 0 && ((1L << _la) & ((1L << INT_NUMBER) | (1L << DECIMAL_NUMBER) | (1L << DOUBLE_QUOTED_TEXT) | (1L << SINGLE_QUOTED_TEXT))) != 0)) ) {
			ErrorHandler.RecoverInline(this);
			}
			else {
				ErrorHandler.ReportMatch(this);
			    Consume();
			}
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class VariableContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public IdentifierContext identifier() {
			return GetRuleContext<IdentifierContext>(0);
		}
		public VariableContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_variable; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterVariable(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitVariable(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitVariable(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public VariableContext variable() {
		VariableContext _localctx = new VariableContext(Context, State);
		EnterRule(_localctx, 10, RULE_variable);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 57;
			identifier();
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public partial class IdentifierContext : ParserRuleContext {
		[System.Diagnostics.DebuggerNonUserCode] public ITerminalNode IDENTIFIER() { return GetToken(RuleConditionParser.IDENTIFIER, 0); }
		public IdentifierContext(ParserRuleContext parent, int invokingState)
			: base(parent, invokingState)
		{
		}
		public override int RuleIndex { get { return RULE_identifier; } }
		[System.Diagnostics.DebuggerNonUserCode]
		public override void EnterRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.EnterIdentifier(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override void ExitRule(IParseTreeListener listener) {
			IRuleConditionListener typedListener = listener as IRuleConditionListener;
			if (typedListener != null) typedListener.ExitIdentifier(this);
		}
		[System.Diagnostics.DebuggerNonUserCode]
		public override TResult Accept<TResult>(IParseTreeVisitor<TResult> visitor) {
			IRuleConditionVisitor<TResult> typedVisitor = visitor as IRuleConditionVisitor<TResult>;
			if (typedVisitor != null) return typedVisitor.VisitIdentifier(this);
			else return visitor.VisitChildren(this);
		}
	}

	[RuleVersion(0)]
	public IdentifierContext identifier() {
		IdentifierContext _localctx = new IdentifierContext(Context, State);
		EnterRule(_localctx, 12, RULE_identifier);
		try {
			EnterOuterAlt(_localctx, 1);
			{
			State = 59;
			Match(IDENTIFIER);
			}
		}
		catch (RecognitionException re) {
			_localctx.exception = re;
			ErrorHandler.ReportError(this, re);
			ErrorHandler.Recover(this, re);
		}
		finally {
			ExitRule();
		}
		return _localctx;
	}

	public override bool Sempred(RuleContext _localctx, int ruleIndex, int predIndex) {
		switch (ruleIndex) {
		case 1: return expr_sempred((ExprContext)_localctx, predIndex);
		}
		return true;
	}
	private bool expr_sempred(ExprContext _localctx, int predIndex) {
		switch (predIndex) {
		case 0: return Precpred(Context, 6);
		case 1: return Precpred(Context, 5);
		case 2: return Precpred(Context, 4);
		case 3: return Precpred(Context, 3);
		case 4: return Precpred(Context, 2);
		}
		return true;
	}

	private static char[] _serializedATN = {
		'\x3', '\x608B', '\xA72A', '\x8133', '\xB9ED', '\x417C', '\x3BE7', '\x7786', 
		'\x5964', '\x3', '*', '@', '\x4', '\x2', '\t', '\x2', '\x4', '\x3', '\t', 
		'\x3', '\x4', '\x4', '\t', '\x4', '\x4', '\x5', '\t', '\x5', '\x4', '\x6', 
		'\t', '\x6', '\x4', '\a', '\t', '\a', '\x4', '\b', '\t', '\b', '\x3', 
		'\x2', '\x3', '\x2', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x5', '\x3', '\x1D', '\n', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', '\x3', 
		'\a', '\x3', '/', '\n', '\x3', '\f', '\x3', '\xE', '\x3', '\x32', '\v', 
		'\x3', '\x3', '\x4', '\x3', '\x4', '\x3', '\x5', '\x3', '\x5', '\x5', 
		'\x5', '\x38', '\n', '\x5', '\x3', '\x6', '\x3', '\x6', '\x3', '\a', '\x3', 
		'\a', '\x3', '\b', '\x3', '\b', '\x3', '\b', '\x2', '\x3', '\x4', '\t', 
		'\x2', '\x4', '\x6', '\b', '\n', '\f', '\xE', '\x2', '\x6', '\x3', '\x2', 
		'\xF', '\x10', '\x3', '\x2', '\x11', '\x12', '\x3', '\x2', '\t', '\xE', 
		'\x4', '\x2', '\x6', '\a', '\'', '(', '\x2', '\x41', '\x2', '\x10', '\x3', 
		'\x2', '\x2', '\x2', '\x4', '\x1C', '\x3', '\x2', '\x2', '\x2', '\x6', 
		'\x33', '\x3', '\x2', '\x2', '\x2', '\b', '\x37', '\x3', '\x2', '\x2', 
		'\x2', '\n', '\x39', '\x3', '\x2', '\x2', '\x2', '\f', ';', '\x3', '\x2', 
		'\x2', '\x2', '\xE', '=', '\x3', '\x2', '\x2', '\x2', '\x10', '\x11', 
		'\x5', '\x4', '\x3', '\x2', '\x11', '\x3', '\x3', '\x2', '\x2', '\x2', 
		'\x12', '\x13', '\b', '\x3', '\x1', '\x2', '\x13', '\x14', '\a', '!', 
		'\x2', '\x2', '\x14', '\x15', '\x5', '\x4', '\x3', '\x2', '\x15', '\x16', 
		'\a', '\"', '\x2', '\x2', '\x16', '\x1D', '\x3', '\x2', '\x2', '\x2', 
		'\x17', '\x18', '\t', '\x2', '\x2', '\x2', '\x18', '\x1D', '\x5', '\x4', 
		'\x3', '\n', '\x19', '\x1A', '\a', '\x14', '\x2', '\x2', '\x1A', '\x1D', 
		'\x5', '\x4', '\x3', '\t', '\x1B', '\x1D', '\x5', '\b', '\x5', '\x2', 
		'\x1C', '\x12', '\x3', '\x2', '\x2', '\x2', '\x1C', '\x17', '\x3', '\x2', 
		'\x2', '\x2', '\x1C', '\x19', '\x3', '\x2', '\x2', '\x2', '\x1C', '\x1B', 
		'\x3', '\x2', '\x2', '\x2', '\x1D', '\x30', '\x3', '\x2', '\x2', '\x2', 
		'\x1E', '\x1F', '\f', '\b', '\x2', '\x2', '\x1F', ' ', '\t', '\x3', '\x2', 
		'\x2', ' ', '/', '\x5', '\x4', '\x3', '\t', '!', '\"', '\f', '\a', '\x2', 
		'\x2', '\"', '#', '\t', '\x2', '\x2', '\x2', '#', '/', '\x5', '\x4', '\x3', 
		'\b', '$', '%', '\f', '\x6', '\x2', '\x2', '%', '&', '\x5', '\x6', '\x4', 
		'\x2', '&', '\'', '\x5', '\x4', '\x3', '\a', '\'', '/', '\x3', '\x2', 
		'\x2', '\x2', '(', ')', '\f', '\x5', '\x2', '\x2', ')', '*', '\a', '\x18', 
		'\x2', '\x2', '*', '/', '\x5', '\x4', '\x3', '\x6', '+', ',', '\f', '\x4', 
		'\x2', '\x2', ',', '-', '\a', '\x1B', '\x2', '\x2', '-', '/', '\x5', '\x4', 
		'\x3', '\x5', '.', '\x1E', '\x3', '\x2', '\x2', '\x2', '.', '!', '\x3', 
		'\x2', '\x2', '\x2', '.', '$', '\x3', '\x2', '\x2', '\x2', '.', '(', '\x3', 
		'\x2', '\x2', '\x2', '.', '+', '\x3', '\x2', '\x2', '\x2', '/', '\x32', 
		'\x3', '\x2', '\x2', '\x2', '\x30', '.', '\x3', '\x2', '\x2', '\x2', '\x30', 
		'\x31', '\x3', '\x2', '\x2', '\x2', '\x31', '\x5', '\x3', '\x2', '\x2', 
		'\x2', '\x32', '\x30', '\x3', '\x2', '\x2', '\x2', '\x33', '\x34', '\t', 
		'\x4', '\x2', '\x2', '\x34', '\a', '\x3', '\x2', '\x2', '\x2', '\x35', 
		'\x38', '\x5', '\n', '\x6', '\x2', '\x36', '\x38', '\x5', '\f', '\a', 
		'\x2', '\x37', '\x35', '\x3', '\x2', '\x2', '\x2', '\x37', '\x36', '\x3', 
		'\x2', '\x2', '\x2', '\x38', '\t', '\x3', '\x2', '\x2', '\x2', '\x39', 
		':', '\t', '\x5', '\x2', '\x2', ':', '\v', '\x3', '\x2', '\x2', '\x2', 
		';', '<', '\x5', '\xE', '\b', '\x2', '<', '\r', '\x3', '\x2', '\x2', '\x2', 
		'=', '>', '\a', '&', '\x2', '\x2', '>', '\xF', '\x3', '\x2', '\x2', '\x2', 
		'\x6', '\x1C', '.', '\x30', '\x37',
	};

	public static readonly ATN _ATN =
		new ATNDeserializer().Deserialize(_serializedATN);


}