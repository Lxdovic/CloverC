namespace Driver;

public sealed class Driver {
    private static void Error(string message) {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine(message);
        Environment.Exit(1);
    }

    public void Run(string[] args) {
        if (args.Length == 0) Error("Usage: clover <source-file>");

        var filePath = args[0];

        if (!Path.Exists(filePath)) Error($"File '{filePath}' does not exist.");

        var shell = new Shell();

        shell.Run($"gcc -E -P {filePath} -o {Path.ChangeExtension(filePath, ".i")}");
    }
}