using System.Text;
using CloverC.Syntax;

namespace CloverC.Helpers;

public static class SyntaxTreePrinter {
    public static void Print(SyntaxTree syntaxTree) {
        var sb = new StringBuilder();

        sb.Append("Program(\n  members: [\n");

        foreach (var member in syntaxTree.Root.Members) PrintMember(sb, member, 2);

        sb.Append("  ]\n)");

        Console.WriteLine(sb.ToString());
    }
    
    public static void Print(SyntaxTree syntaxTree, StringBuilder sb) {
        sb.Append("Program(\n  members: [\n");

        foreach (var member in syntaxTree.Root.Members) PrintMember(sb, member, 2);

        sb.Append("  ]\n)");

        Console.WriteLine(sb.ToString());
    }

    private static void PrintMember(StringBuilder sb, MemberSyntax member, int indent) {
        switch (member.Kind) {
            case SyntaxKind.FunctionDeclaration:
                PrintFunctionDeclaration(sb, member as FunctionDeclarationSyntax, indent);
                break;
        }
    }

    private static void PrintFunctionDeclaration(StringBuilder sb, FunctionDeclarationSyntax? function, int indent) {
        if (function == null) return;

        sb.Append(
            $"{new string(' ', indent * 2)}Function(\n{new string(' ', (indent + 1) * 2)}identifier={function.Identifier.Value}\n{new string(' ', (indent + 1) * 2)}body=");
        PrintBlockStatement(sb, function.Body, indent + 2);
        sb.Append($"\n{new string(' ', indent * 2)})\n");
    }

    private static void PrintBlockStatement(StringBuilder sb, BlockStatementSyntax body, int indent) {
        sb.Append($"Block(\n{new string(' ', indent * 2)}statements: [");

        foreach (var statement in body.Statements) PrintStatementSyntax(sb, statement, indent + 1);

        sb.Append($"\n{new string(' ', indent * 2)}]");
        sb.Append($"\n{new string(' ', (indent - 1) * 2)})");
    }

    private static void PrintStatementSyntax(StringBuilder sb, StatementSyntax statement, int indent) {
        sb.Append($"\n{new string(' ', indent * 2)}");

        switch (statement.Kind) {
            case SyntaxKind.ReturnStatement:
                PrintReturnStatement(sb, statement as ReturnStatementSyntax, indent);
                break;
        }
    }

    private static void PrintReturnStatement(StringBuilder sb, ReturnStatementSyntax? statement, int indent) {
        if (statement == null) return;

        sb.Append("Return(\n");

        if (statement.Expression != null) PrintConstantSyntax(sb, (ConstantSyntax)statement.Expression, indent + 1);

        sb.Append($"\n{new string(' ', indent * 2)})");
    }

    private static void PrintConstantSyntax(StringBuilder sb, ConstantSyntax constantStatement, int indent) {
        sb.Append($"{new string(' ', indent * 2)}Constant({constantStatement.Value.Value})");
    }
}