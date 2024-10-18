using System.Data;
using System.Text.RegularExpressions;

namespace CloverC.Syntax;

public sealed class Lexer(string document) {
    private static readonly Dictionary<SyntaxKind, Regex> Regexes = new() {
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

    private string OriginalDocument { get; } = document;
    private string Document { get; set; } = document;

    public SyntaxToken[] Lex() {
        List<SyntaxToken> tokens = [];

        while (Document.Length > 0) {
            if (char.IsWhiteSpace(Document[0])) {
                Document = Document.TrimStart();

                continue;
            }

            var (kind, regex) = Regexes
                .Aggregate((l, r) => {
                    return l.Value.Match(Document).Length <= r.Value.Match(Document).Length
                        ? r
                        : l;
                });


            var match = regex.Match(Document);

            if (match.Length == 0) {
                var doc = Document.Split(Environment.NewLine)[0];
                var exceptionIndex = OriginalDocument.Split(doc)[0].Length;
                var exceptionTemplate = "Unexpected Token...";
                throw new SyntaxErrorException(
                    $"\n{exceptionTemplate}{doc}\n{new string(' ', exceptionTemplate.Length)}{new string('^', doc.Length)}"
                );
            }

            Document = regex.Replace(Document, "");

            tokens.Add(new SyntaxToken(kind, match.Value));
        }

        return [..tokens, new SyntaxToken(SyntaxKind.EndOfFile)];
    }
}