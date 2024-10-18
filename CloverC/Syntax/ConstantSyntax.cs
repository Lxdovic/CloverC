namespace CloverC.Syntax;

public sealed class ConstantSyntax(SyntaxToken value) : ExpressionSyntax {
    public SyntaxToken Value { get; } = value;
    public override SyntaxKind Kind => SyntaxKind.Constant;
}