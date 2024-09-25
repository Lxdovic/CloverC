namespace CloverC.Syntax;

public sealed class SyntaxToken {
    public SyntaxKind Kind { get; }

    internal SyntaxToken(SyntaxKind kind) {
        Kind = kind;
    }
}