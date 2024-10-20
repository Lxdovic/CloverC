using System.Data;
using System.Text;
using CloverC.Helpers;
using CloverC.Syntax;

namespace CloverC.Tests;

public class ParserTests {
    [Theory]
    [InlineData(@"int main(void) { return")]
    [InlineData(@"int main(void) { return 2; } foo")]
    [InlineData(@"int 3 (void) { return 0; }")]
    [InlineData(@"int main(void) { RETURN 0; }")]
    [InlineData(@"main(void) { return 0; }")]
    [InlineData(@"int main(void) { returns 0; }")]
    [InlineData(@"int main (void) { return 0 }")]
    [InlineData(@"int main(void) { return int; }")]
    [InlineData(@"int main(void) { retur n 0; }")]
    [InlineData(@"int main )( { return 0; }")]
    [InlineData(@"int main(void) { return 0;")]
    [InlineData(@"int main( { return 0; }")]
    public void ParserThrowsSyntaxException<T>(string text) {
        var lexer = new Lexer(text);
        var parser = new Parser(lexer.Lex());

        Assert.Throws<SyntaxErrorException>(parser.Parse);
    }

    [Fact]
    public void ParserReturnsCorrectSyntaxTree() {
        var lexer = new Lexer("int main(void) { return 0; }");
        var parser = new Parser(lexer.Lex());
        var syntaxTree = parser.Parse();

        Assert.NotNull(syntaxTree);
        Assert.NotNull(syntaxTree.Root);

        var sb = new StringBuilder();

        SyntaxTreePrinter.Print(syntaxTree, sb);

        Assert.Equal(@"Program(
  members: [
    Function(
      identifier=main
      body=Block(
        statements: [
          Return(
            Constant(0)
          )
        ]
      )
    )
  ]
)",
            sb.ToString()
        );
    }
}