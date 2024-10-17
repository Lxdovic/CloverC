namespace CloverC.Syntax;

public sealed class FunctionDeclarationSyntax(
    SyntaxToken type,
    SyntaxToken identifier,
    SyntaxToken openParenthesis,
    SyntaxToken closeParenthesis,
    BlockStatementSyntax body)
    : MemberSyntax {
    public SyntaxToken Type { get; } = type;
    public SyntaxToken Identifier { get; } = identifier;
    public SyntaxToken OpenParenthesis { get; } = openParenthesis;
    public SyntaxToken CloseParenthesis { get; } = closeParenthesis;
    public BlockStatementSyntax Body { get; } = body;

    public override SyntaxKind Kind { get; } = SyntaxKind.FunctionDeclaration;
}