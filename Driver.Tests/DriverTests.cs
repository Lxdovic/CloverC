using Xunit.Abstractions;

namespace Driver.Tests;

public class DriverTests {
    private readonly ITestOutputHelper _output;

    public DriverTests(ITestOutputHelper output) {
        _output = output;
    }

    [Fact]
    public void DriverTakesInput() {
        var driver = new Driver();
        var filePath = Path.Combine(Environment.CurrentDirectory, "Resources/input/tet.c");

        using var sw = new StringWriter();

        Console.SetOut(sw);

        var exception = Record.Exception(() => driver.Run([filePath]));

        Assert.Null(exception);
        Assert.Equal(0, Environment.ExitCode);
    }
}