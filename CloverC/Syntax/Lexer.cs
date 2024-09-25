using System.Text;
using System.Text.RegularExpressions;

namespace CloverC.Syntax;

public sealed class Lexer(string document) {
    private int _position;

    private (SyntaxKind kind, Regex regex)[] _regexes = {
        (SyntaxKind.Number, new Regex(@"[a-zA-Z_]\w*\b", RegexOptions.Multiline))
    };

    private char Current => Peek(0);
    private char Next => Peek(1);

    private string Document { get; } = document;

    private char Peek(int offset) {
        var index = _position + offset;
        if (index >= Document.Length) return '\0';

        return Document[index];
    }

    private void PrintTokens(SyntaxToken[] tokens) {
        foreach (var token in tokens)
            Console.WriteLine($"kind: {token.Kind}, text: {token.Text}, value: {token.Value}");
    }

    public SyntaxToken[] Lex() {
        var tokens = new List<SyntaxToken>();
        _position = 0;

        Console.WriteLine(Document);

        while (Current != '\0') {
            if (char.IsWhiteSpace(Current)) {
                _position++;
                continue;
            }

            var text = Current.ToString();

            switch (Current) {
                case '\0':
                    tokens.Add(new SyntaxToken(SyntaxKind.EndOfFile, text));
                    _position++;
                    break;
                case '(':
                    tokens.Add(new SyntaxToken(SyntaxKind.OpenParenthesis, text));
                    _position++;
                    break;
                case ')':
                    tokens.Add(new SyntaxToken(SyntaxKind.CloseParenthesis, text));
                    _position++;
                    break;
                case ';':
                    tokens.Add(new SyntaxToken(SyntaxKind.SemiColon, text));
                    _position++;
                    break;
                case '{':
                    tokens.Add(new SyntaxToken(SyntaxKind.OpenCurlyBrackets, text));
                    _position++;
                    break;
                case '}':
                    tokens.Add(new SyntaxToken(SyntaxKind.CloseCurlyBrackets, text));
                    _position++;
                    break;
                case '0':
                case '1':
                case '2':
                case '3':
                case '4':
                case '5':
                case '6':
                case '7':
                case '8':
                case '9':
                    ReadDigit(ref tokens);
                    break;
                default:
                    if (char.IsLetter(Current)) ReadIdentifierOrKeyword(ref tokens);
                    break;
            }
        }

        PrintTokens(tokens.ToArray());

        return tokens.ToArray();
    }

    private void ReadIdentifierOrKeyword(ref List<SyntaxToken> tokens) {
        var sb = new StringBuilder();

        for (; char.IsLetterOrDigit(Current) && Current != '\0'; _position++) sb.Append(Current);

        var text = sb.ToString();
        tokens.Add(new SyntaxToken(SyntaxFacts.GetKind(text), text));
    }

    private void ReadDigit(ref List<SyntaxToken> tokens) {
        var sb = new StringBuilder();

        for (; char.IsDigit(Current) && Current != '\0'; _position++) sb.Append(Current);

        var text = sb.ToString();
        tokens.Add(new SyntaxToken(SyntaxKind.Number, text, int.Parse(text)));
    }
}