namespace CloverC.Syntax;

public class SyntaxToken {
    internal SyntaxToken(SyntaxKind kind, string? text = null, object? value = null) {
        Kind = kind;
        Value = value;
        Text = text;
    }

    public SyntaxKind Kind { get; }
    public object? Value { get; }
    public string? Text { get; }
}