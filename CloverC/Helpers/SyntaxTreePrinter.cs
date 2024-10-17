using System.Text;
using CloverC.Syntax;

namespace CloverC.Helpers;

public static class SyntaxTreePrinter {
    public static void Print(SyntaxTree syntaxTree) {
        foreach (var member in syntaxTree.Root.Members) {
            var sb = new StringBuilder();
            PrintMember(sb, member);

            Console.WriteLine(sb.ToString());
        }
    }

    private static void PrintMember(StringBuilder sb, MemberSyntax member) {
        switch (member.Kind) {
            case SyntaxKind.FunctionDeclaration:
                PrintFunctionDeclaration(sb, member as FunctionDeclarationSyntax);
                break;
        }
    }

    private static void PrintFunctionDeclaration(StringBuilder sb, FunctionDeclarationSyntax? function) {
        if (function == null) return;

        sb.Append($"Function(\n  identifier={function.Identifier.Value},\n  body=");
        PrintBlockStatement(sb, function.Body, 2);
        sb.Append("\n)");
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

        if (statement.Constant != null) PrintConstantSyntax(sb, statement.Constant, indent + 1);

        sb.Append($"\n{new string(' ', indent * 2)})");
    }

    private static void PrintConstantSyntax(StringBuilder sb, ConstantSyntax constantStatement, int indent) {
        sb.Append($"{new string(' ', indent * 2)}Constant({constantStatement.Value.Value})");
    }
}