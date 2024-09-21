namespace CloverC;

public static class Program {
    public static void Main(string[] args) {
        if (args.Length < 1) {
            Console.WriteLine("usage: clover <input.c> [<output>]");
            Environment.Exit(1);
        }

        if (!File.Exists(args[0])) {
            Console.WriteLine("ERROR: input file not found");
            Environment.Exit(1);
        }

        new Clover().Run(args, args[0], args.Length > 1 ? args[1] : null);
    }
}