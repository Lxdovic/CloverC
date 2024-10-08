using System.Collections.Immutable;

namespace CloverC.Syntax;

public sealed class CompilationUnitSyntax(ImmutableArray<SyntaxNode> members) {
    public ImmutableArray<SyntaxNode> Members { get; } = members;

}