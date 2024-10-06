using System.Data;
using CloverC.Syntax;

namespace CloverC.Tests;

public class LexerTests {
    [Theory]
    [InlineData(@"int main(void) { return 0@1; }")]
    [InlineData(@"\")]
    [InlineData(@"`")]
    [InlineData(@"int main(void) { return 1foo; }")]
    [InlineData(@"int main(void) { return @b; }")]
    public void LexerThrowsSyntaxException<T>(string text) {
        var lexer = new Lexer(text);

        Assert.Throws<SyntaxErrorException>(lexer.Lex);
    }
}