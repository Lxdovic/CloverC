using System.Collections.Immutable;

namespace CloverC.Syntax;

public sealed class BlockStatementSyntax(
    ImmutableArray<StatementSyntax> statements) : StatementSyntax {
    public ImmutableArray<StatementSyntax> Statements { get; } = statements;

    public override SyntaxKind Kind { get; } = SyntaxKind.BlockStatement;
}