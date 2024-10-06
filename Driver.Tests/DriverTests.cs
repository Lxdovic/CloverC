using Xunit.Abstractions;

namespace Driver.Tests;

public class DriverTests(ITestOutputHelper output) {
    // private readonly ITestOutputHelper _output = output;
    //
    // [Fact]
    // public void DriverTakesInput() {
    //     var driver = new Driver();
    //     var filePath = Path.Combine(Environment.CurrentDirectory, "Resources/input/test.c");
    //
    //     var exception = Record.Exception(() => driver.Run([filePath]));
    //
    //     Assert.Null(exception);
    //     Assert.Equal(0, Environment.ExitCode);
    // }
}