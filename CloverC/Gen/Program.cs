using CloverC.Helpers;

namespace CloverC.Gen;

public class Program(FunctionDefinition function) {
    public FunctionDefinition Function { get; } = function;
}