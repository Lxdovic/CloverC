using CloverC.Syntax;

namespace CloverC.Gen;

public class Mov(Exp expression, Register register) : Instruction {
    public Exp Expression { get; } = expression;
    public Register Register { get; } = register;
    public override SyntaxKind Kind { get; } = SyntaxKind.MovInstruction;
}