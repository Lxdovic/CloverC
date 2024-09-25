namespace CloverC.Syntax;

internal static class SyntaxFacts {
    internal static SyntaxKind GetKind(string text) {
        return text switch {
            _ => SyntaxKind.Identifier
        };
    }
}