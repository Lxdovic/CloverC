using CloverC.Syntax;

namespace CloverC.Gen;

public abstract class Instruction {
    public abstract SyntaxKind Kind { get; }
}