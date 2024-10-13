using System.Collections.Immutable;

namespace CloverC.Syntax;

public sealed class CompilationUnitSyntax : SyntaxNode {
    public CompilationUnitSyntax(ImmutableArray<MemberSyntax> members) {
        Members = members;
    }

    public override SyntaxKind Kind => SyntaxKind.CompilationUnit;

    public ImmutableArray<MemberSyntax> Members { get; set; }
}