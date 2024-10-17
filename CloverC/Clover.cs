using CloverC.Syntax;

namespace CloverC;

public class Clover {
    private static void Error(string message) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Environment.Exit(1);
    }

    public void Run(string[] args, string input, string? output) {
        if (!Path.Exists(input)) Error($"File '{input}' does not exist.");

        var document = File.ReadAllText(input);
        var generated = "";

        if (args.Contains("--lex")) {
            var tokens = new Lexer(document).Lex();
            Environment.Exit(0);
        }

        if (args.Contains("--parse")) {
            var syntaxTree = new Parser(new Lexer(document).Lex()).Parse();
            Environment.Exit(0);
        }

        if (args.Contains("--codegen")) {
            generated = "";
            Environment.Exit(0);
        }

        var outputPath = output ?? Path.ChangeExtension(input, "");

        File.Create(outputPath);
        File.WriteAllText(outputPath, generated);
    }
}