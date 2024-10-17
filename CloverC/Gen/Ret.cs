using CloverC.Syntax;

namespace CloverC.Gen;

public class Ret(Mov mov) : Instruction {
    public Mov Mov { get; } = mov;
    public override SyntaxKind Kind { get; } = SyntaxKind.ReturnInstruction;
}