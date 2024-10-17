using System.Collections.Immutable;

namespace CloverC.Syntax;

public sealed class BlockStatementSyntax(
    SyntaxToken openBraceToken,
    ImmutableArray<StatementSyntax> statements,
    SyntaxToken closeBraceToken) : StatementSyntax {
    public SyntaxToken OpenBraceToken { get; } = openBraceToken;
    public ImmutableArray<StatementSyntax> Statements { get; } = statements;
    public SyntaxToken CloseBraceToken { get; } = closeBraceToken;

    public override SyntaxKind Kind { get; } = SyntaxKind.BlockStatement;
}