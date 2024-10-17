namespace CloverC.Syntax;

public sealed class ReturnStatementSyntax(SyntaxToken returnKeyword, ConstantSyntax? constant = null)
    : StatementSyntax {
    public override SyntaxKind Kind => SyntaxKind.ReturnStatement;
    public SyntaxToken ReturnKeyword { get; } = returnKeyword;
    public ConstantSyntax? Constant { get; } = constant;
}