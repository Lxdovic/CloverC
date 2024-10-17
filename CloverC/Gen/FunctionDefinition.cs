namespace CloverC.Gen;

public class FunctionDefinition(string name, Instruction[] instructions) {
    public string Name { get; } = name;
    public Instruction[] Instructions { get; } = instructions;
}