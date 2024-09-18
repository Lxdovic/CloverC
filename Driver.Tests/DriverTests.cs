namespace Driver.Tests;

public class DriverTests {
    [Fact]
    public void DriverTakesInput() {
        var driver = new Driver();
        driver.Run(["test.c"]);
    }
}