using CloverC;

namespace Driver;

public sealed class Driver {
    private static readonly Shell Shell = new();

    private static void Error(string message) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Environment.Exit(1);
    }

    private void Preprocess(string input, string output) {
        Shell.Run($"gcc -E -P {input} -o {output}");
    }

    private void Compile(string input, string output) {
        new Clover().Run([input, output]);
    }

    private void Assemble(string input, string output) {
        Shell.Run($"gcc {input} -o {output}");
    }

    public void Run(string[] args) {
        if (args.Length == 0) Error("Usage: clover <source-file>");

        var inputFilePath = args[0];

        if (!Path.Exists(inputFilePath)) Error($"File '{inputFilePath}' does not exist.");

        var preprocessedFilePath = Path.ChangeExtension(inputFilePath, ".i");
        var compiledFilePath = Path.ChangeExtension(preprocessedFilePath, ".s");
        var outputFilePath = Path.ChangeExtension(preprocessedFilePath, "");

        Preprocess(inputFilePath, preprocessedFilePath);
        Compile(preprocessedFilePath, compiledFilePath);
        Assemble(compiledFilePath, outputFilePath);

        File.Delete(preprocessedFilePath);
        File.Delete(compiledFilePath);
    }
}