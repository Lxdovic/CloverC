namespace CloverC.Syntax;

public sealed class Lexer {
    private readonly Dictionary<char, (char[], SyntaxKind)> _syntaxKinds = new() {
        { '\0', ([], SyntaxKind.EndOfFile) },
        { '=', (['='], SyntaxKind.EqualsEquals) },
        { '=', (['>'], SyntaxKind.GreaterEquals) }
    };

    private int _position;
    private int _start;

    public Lexer(string document) {
        Document = document;
    }


    internal string Document { get; }

    private char Peek(int offset) {
        var index = _position - offset;

        if (index >= Document.Length) return '\0';

        return Document[index];
    }

    public SyntaxToken[] Lex() {
        // if (_syntaxKinds.TryGetValue(Peek(0), out var syntaxKind)) {
        //     foreach (var (chars, kind) in syntaxKind) {
        //         var match = true;
        //         for (var i = 0; i < chars.Length; i++) {
        //             if (Peek(i) != chars[i]) {
        //                 match = false;
        //                 break;
        //             }
        //         }
        //
        //         if (match) {
        //             _start = _position;
        //             _position += chars.Length;
        //             return new SyntaxToken(kind);
        //         }
        //     }
        //     
        // }

        return [];
    }
}