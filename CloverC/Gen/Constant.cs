namespace CloverC.Gen;

public class Constant(int value) : Exp {
    public int Value { get; } = value;
}