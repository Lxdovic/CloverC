using System.Collections.Immutable;
using System.Data;
using CloverC.Helpers;

namespace CloverC.Syntax;

public sealed class Parser {
    private readonly SyntaxTree _syntaxTree = new();
    private readonly SyntaxToken[] _tokens;
    private int _position;

    public Parser(SyntaxToken[] tokens) {
        _tokens = tokens;
    }

    private SyntaxToken Current => Peek(0);
    private SyntaxToken Next => Peek(1);

    private SyntaxToken Peek(int offset) {
        var index = _position + offset;
        if (index >= _tokens.Length) return _tokens[_tokens.Length - 1];
        return _tokens[index];
    }

    public SyntaxTree Parse() {
        _syntaxTree.Root = ParseCompilationUnit();
        SyntaxTreePrinter.Print(_syntaxTree);

        return _syntaxTree;
    }

    private SyntaxToken MatchTokens(SyntaxKind[] kinds) {
        if (kinds.Any(k => k == Current.Kind)) return NextToken();

        throw new SyntaxErrorException("Unexpected Token");
    }

    private SyntaxToken MatchToken(SyntaxKind kind) {
        if (Current.Kind == kind) return NextToken();

        throw new SyntaxErrorException("Unexpected Token");
    }

    private CompilationUnitSyntax ParseCompilationUnit() {
        var members = ParseMembers();

        return new CompilationUnitSyntax(members);
    }

    private SyntaxToken NextToken() {
        var current = Current;
        _position++;
        return current;
    }

    private ImmutableArray<MemberSyntax> ParseMembers() {
        var members = ImmutableArray.CreateBuilder<MemberSyntax>();

        while (Current.Kind != SyntaxKind.EndOfFile) {
            var startToken = Current;
            var member = ParseMember();

            members.Add(member);

            if (Current == startToken) NextToken();
        }

        return members.ToImmutable();
    }

    private MemberSyntax ParseMember() {
        return ParseFunctionDeclaration();
    }

    private MemberSyntax ParseFunctionDeclaration() {
        var type = MatchTokens([SyntaxKind.IntKeyword, SyntaxKind.VoidKeyword]);
        var identifier = MatchToken(SyntaxKind.Identifier);
        MatchToken(SyntaxKind.OpenParenthesis);
        MatchToken(SyntaxKind.CloseParenthesis);
        var body = ParseBlockStatement();

        return new FunctionDeclarationSyntax(type, identifier, body);
    }

    private BlockStatementSyntax ParseBlockStatement() {
        var statements = ImmutableArray.CreateBuilder<StatementSyntax>();
        MatchToken(SyntaxKind.OpenCurlyBrackets);

        while (Current.Kind != SyntaxKind.EndOfFile && Current.Kind != SyntaxKind.CloseCurlyBrackets) {
            var startToken = Current;
            var statement = ParseStatement();

            statements.Add(statement);

            // if the statement was not parsed correctly, we skip to the next token
            if (Current == startToken) NextToken();
        }

        MatchToken(SyntaxKind.CloseCurlyBrackets);

        return new BlockStatementSyntax(statements.ToImmutable());
    }

    private StatementSyntax ParseStatement() {
        switch (Current.Kind) {
            case SyntaxKind.ReturnKeyword:
                return ParseReturnStatement();
            default:
                return null;
        }
    }

    private ConstantSyntax ParseConstant() {
        var constant = MatchToken(SyntaxKind.Constant);

        return new ConstantSyntax(constant);
    }

    private StatementSyntax ParseReturnStatement() {
        var keyword = MatchToken(SyntaxKind.ReturnKeyword);
        var expression = Current.Kind == SyntaxKind.SemiColon ? null : ParseExpression();

        MatchToken(SyntaxKind.SemiColon);

        return new ReturnStatementSyntax(keyword, expression);
    }

    private ExpressionSyntax ParseExpression() {
        // no expressions yet, only constants
        return ParseConstant();
    }
}