namespace CloverC.Syntax;

public enum SyntaxKind {
    EndOfFile,
    OpenParenthesis,
    CloseParenthesis,
    OpenCurlyBrackets,
    CloseCurlyBrackets,
    Identifier,
    Constant,
    IntKeyword,
    VoidKeyword,
    ReturnKeyword,
    SemiColon,
    CompilationUnit,
    ExpressionStatement,
    ReturnStatement,
    FunctionDeclaration,
    BlockStatement
}