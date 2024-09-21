namespace CloverC;

public class Clover {
    private static void Error(string message) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Environment.Exit(1);
    }

    public string Run(string[] args) {
        if (args.Length == 0) Error("Usage: clover <source-file>");

        var inputFilePath = args[0];
        var outputFilePath = args.Length > 1 ? args[1] : Path.ChangeExtension(inputFilePath, ".s");

        if (!Path.Exists(inputFilePath)) Error($"File '{inputFilePath}' does not exist.");

        File.Create(outputFilePath);

        return outputFilePath;
    }
}