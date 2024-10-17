namespace CloverC.Syntax;

public sealed class ConstantSyntax(SyntaxToken value) : SyntaxNode {
    public SyntaxToken Value { get; } = value;
    public override SyntaxKind Kind => SyntaxKind.Constant;
}