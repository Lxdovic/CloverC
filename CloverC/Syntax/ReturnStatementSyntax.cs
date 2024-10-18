namespace CloverC.Syntax;

public sealed class ReturnStatementSyntax(SyntaxToken returnKeyword, ExpressionSyntax? expression = null)
    : StatementSyntax {
    public override SyntaxKind Kind => SyntaxKind.ReturnStatement;
    public SyntaxToken ReturnKeyword { get; } = returnKeyword;
    public ExpressionSyntax? Expression { get; } = expression;
}