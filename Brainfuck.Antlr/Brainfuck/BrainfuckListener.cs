//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     ANTLR Version: 4.5.3
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// Generated from grammars/BrainfuckOptimized.g4 by ANTLR 4.5.3

// Unreachable code detected
#pragma warning disable 0162
// The variable '...' is assigned but its value is never used
#pragma warning disable 0219
// Missing XML comment for publicly visible type or member '...'
#pragma warning disable 1591
// Ambiguous reference in cref attribute
#pragma warning disable 419

using Antlr4.Runtime.Misc;
using IParseTreeListener = Antlr4.Runtime.Tree.IParseTreeListener;
using IToken = Antlr4.Runtime.IToken;

/// <summary>
/// This interface defines a complete listener for a parse tree produced by
/// <see cref="BrainfuckOptimizedParser"/>.
/// </summary>
[System.CodeDom.Compiler.GeneratedCode("ANTLR", "4.5.3")]
[System.CLSCompliant(false)]
public interface IBrainfuckOptimizedListener : IParseTreeListener {
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.analyze"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAnalyze([NotNull] BrainfuckOptimizedParser.AnalyzeContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.analyze"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAnalyze([NotNull] BrainfuckOptimizedParser.AnalyzeContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterExpr([NotNull] BrainfuckOptimizedParser.ExprContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.expr"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitExpr([NotNull] BrainfuckOptimizedParser.ExprContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.next"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterNext([NotNull] BrainfuckOptimizedParser.NextContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.next"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitNext([NotNull] BrainfuckOptimizedParser.NextContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.prev"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrev([NotNull] BrainfuckOptimizedParser.PrevContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.prev"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrev([NotNull] BrainfuckOptimizedParser.PrevContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.add"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterAdd([NotNull] BrainfuckOptimizedParser.AddContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.add"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitAdd([NotNull] BrainfuckOptimizedParser.AddContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.sub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSub([NotNull] BrainfuckOptimizedParser.SubContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.sub"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSub([NotNull] BrainfuckOptimizedParser.SubContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.print"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterPrint([NotNull] BrainfuckOptimizedParser.PrintContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.print"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitPrint([NotNull] BrainfuckOptimizedParser.PrintContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.read"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterRead([NotNull] BrainfuckOptimizedParser.ReadContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.read"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitRead([NotNull] BrainfuckOptimizedParser.ReadContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.sloop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSloop([NotNull] BrainfuckOptimizedParser.SloopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.sloop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSloop([NotNull] BrainfuckOptimizedParser.SloopContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.eloop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterEloop([NotNull] BrainfuckOptimizedParser.EloopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.eloop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitEloop([NotNull] BrainfuckOptimizedParser.EloopContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.reset_value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterReset_value([NotNull] BrainfuckOptimizedParser.Reset_valueContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.reset_value"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitReset_value([NotNull] BrainfuckOptimizedParser.Reset_valueContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterLoop([NotNull] BrainfuckOptimizedParser.LoopContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.loop"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitLoop([NotNull] BrainfuckOptimizedParser.LoopContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.seq_inc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSeq_inc([NotNull] BrainfuckOptimizedParser.Seq_incContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.seq_inc"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSeq_inc([NotNull] BrainfuckOptimizedParser.Seq_incContext context);
	/// <summary>
	/// Enter a parse tree produced by <see cref="BrainfuckOptimizedParser.seq_dec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void EnterSeq_dec([NotNull] BrainfuckOptimizedParser.Seq_decContext context);
	/// <summary>
	/// Exit a parse tree produced by <see cref="BrainfuckOptimizedParser.seq_dec"/>.
	/// </summary>
	/// <param name="context">The parse tree.</param>
	void ExitSeq_dec([NotNull] BrainfuckOptimizedParser.Seq_decContext context);
}
