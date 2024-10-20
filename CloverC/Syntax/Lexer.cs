using System.Data;
using System.Text.RegularExpressions;

namespace CloverC.Syntax;

public sealed class Lexer(string document) {
    private static readonly Dictionary<SyntaxKind, Regex> TokenPatterns = new() {
        { SyntaxKind.Identifier, new Regex(@"\A[a-zA-Z_]\w*\b") },
        { SyntaxKind.Constant, new Regex(@"\A[0-9]+\b") },
        { SyntaxKind.IntKeyword, new Regex(@"\Aint\b") },
        { SyntaxKind.VoidKeyword, new Regex(@"\Avoid\b") },
        { SyntaxKind.ReturnKeyword, new Regex(@"\Areturn\b") },
        { SyntaxKind.OpenParenthesis, new Regex(@"\A\(") },
        { SyntaxKind.CloseParenthesis, new Regex(@"\A\)") },
        { SyntaxKind.OpenCurlyBrackets, new Regex(@"\A{") },
        { SyntaxKind.CloseCurlyBrackets, new Regex(@"\A}") },
        { SyntaxKind.SemiColon, new Regex(@"\A;") }
    };

    private string Document { get; set; } = document;

    private KeyValuePair<SyntaxKind, Regex> ClassifyToken() {
        var token = TokenPatterns
            .Aggregate((l, r) =>
                l.Value.Match(Document).Length <=
                r.Value.Match(Document).Length
                    ? r
                    : l
            );

        return token;
    }

    public SyntaxToken[] Lex() {
        List<SyntaxToken> tokens = [];

        while (Document.Length > 0) {
            if (char.IsWhiteSpace(Document[0])) {
                Document = Document.TrimStart();
                continue;
            }

            var (kind, regex) = ClassifyToken();
            var match = regex.Match(Document);

            if (match.Length == 0) throw new SyntaxErrorException("Unexpected Token");

            tokens.Add(new SyntaxToken(kind, match.Value));

            Document = Document[match.Length..];
        }
        
        return [..tokens, new SyntaxToken(SyntaxKind.EndOfFile)];
    }
}