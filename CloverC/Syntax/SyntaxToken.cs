namespace CloverC.Syntax;

public class SyntaxToken {
    internal SyntaxToken(SyntaxKind kind, string? value = null) {
        Kind = kind;
        Value = value;
    }

    public SyntaxKind Kind { get; }
    public object? Value { get; }
}