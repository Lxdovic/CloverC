namespace CloverC.Syntax;

public sealed class FunctionDeclarationSyntax(
    SyntaxToken type,
    SyntaxToken identifier,
    BlockStatementSyntax body)
    : MemberSyntax {
    public SyntaxToken Type { get; } = type;
    public SyntaxToken Identifier { get; } = identifier;
    public BlockStatementSyntax Body { get; } = body;

    public override SyntaxKind Kind { get; } = SyntaxKind.FunctionDeclaration;
}