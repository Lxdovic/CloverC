using System.Collections.Immutable;
using System.Data;

namespace CloverC.Syntax;

public sealed class Parser {
    private readonly ImmutableArray<SyntaxToken> _tokens;
    private int _position;
    private SyntaxTree _syntaxTree;

    private SyntaxToken Current => Peek(0);

    private SyntaxToken Peek(int offset) {
        var index = _position + offset;
        if (index >= _tokens.Length) return _tokens[_tokens.Length - 1];
        return _tokens[index];
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
        return ParseGlobalStatement();
    }

    private MemberSyntax ParseGlobalStatement() {
        var statement = ParseStatement();

        return new GlobalStatementSyntax(statement);
    }

    private StatementSyntax ParseStatement() {
        switch (Current.Kind) {
            case SyntaxKind.ReturnKeyword:
                return ParseReturnStatement();
            default: return ParseExpressionStatement();
        }
    }

    private StatementSyntax ParseReturnStatement() {
        var keyword = MatchToken(SyntaxKind.ReturnKeyword);
        var expression = Current.Kind == SyntaxKind.SemiColon ? null : ParseExpression();

        return new ReturnStatementSyntax(keyword, expression!);
    }
    
    private ExpressionStatementSyntax ParseExpressionStatement() {
        var expression = ParseExpression();

        return new ExpressionStatementSyntax(expression);
    }

    private ExpressionSyntax ParseExpression() {
        return ParseConstant();
    }

    private ConstantSyntax ParseConstant() {
        var constant = MatchToken(SyntaxKind.Constant);
        
        return new ConstantSyntax(constant);
    }

    private SyntaxToken MatchToken(SyntaxKind kind) {
        if (Current.Kind == kind) return NextToken();

        throw new SyntaxErrorException("Unexpected Token");
    }
}

public sealed class ReturnStatementSyntax : StatementSyntax {
    public ReturnStatementSyntax(SyntaxToken returnKeyword, ExpressionSyntax expression) {
        ReturnKeyword = returnKeyword;
        Expression = expression;
    }

    public override SyntaxKind Kind => SyntaxKind.ReturnStatement;
    public SyntaxToken ReturnKeyword { get; }
    public ExpressionSyntax Expression { get; }
}

public sealed class GlobalStatementSyntax : MemberSyntax {
    public GlobalStatementSyntax(StatementSyntax statement) {
        Statement = statement;
    }

    public StatementSyntax Statement { get; }
    public override SyntaxKind Kind => SyntaxKind.GlobalStatement;
}