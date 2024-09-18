
namespace CloverC.Syntax;

internal enum SyntaxKind {
    EndOfFile,
    Number,
    EqualsEquals,
    GreaterEquals
}

internal sealed class SyntaxToken {
    public SyntaxKind Kind { get; }

    internal SyntaxToken(SyntaxKind kind) {
        Kind = kind;
    }
}

internal sealed class Lexer {
    private int _position;
    private int _start;
    private readonly Dictionary<char, (char[], SyntaxKind)> _syntaxKinds = new() {
        { '\0', ([], SyntaxKind.EndOfFile) },
        { '=', (['='], SyntaxKind.EqualsEquals) },
        { '=', (['>'], SyntaxKind.GreaterEquals) },
    };
    
    
    internal string Document { get; }

    internal Lexer(string document) {
        Document = document;
    }

    private char Peek(int offset) {
        var index = _position - offset;

        if (index >= Document.Length) return '\0';

        return Document[index];
    }
    
    // internal SyntaxToken Lex() {
    //     // if (_syntaxKinds.TryGetValue(Peek(0), out var syntaxKind)) {
    //     //     foreach (var (chars, kind) in syntaxKind) {
    //     //         var match = true;
    //     //         for (var i = 0; i < chars.Length; i++) {
    //     //             if (Peek(i) != chars[i]) {
    //     //                 match = false;
    //     //                 break;
    //     //             }
    //     //         }
    //     //
    //     //         if (match) {
    //     //             _start = _position;
    //     //             _position += chars.Length;
    //     //             return new SyntaxToken(kind);
    //     //         }
    //     //     }
    //     //     
    //     // }
    // }
}