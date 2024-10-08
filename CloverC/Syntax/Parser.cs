namespace CloverC.Syntax;

public sealed class Parser {
    private SyntaxTree _syntaxTree = new();
    
    public SyntaxTree Parse(SyntaxToken[] tokens) {
        _syntaxTree = new SyntaxTree();
        
        foreach (var token in tokens) {
            switch (token.Kind) {
                case SyntaxKind.Constant: ParseConstant(token);
                    break;
            }
        }

        return _syntaxTree;
    }

    public void ParseMembers() { }

    private void ParseConstant(SyntaxToken token) {
        
    }
}