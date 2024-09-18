using System.Diagnostics;

namespace Driver;

public class Shell {
    public ProcessStartInfo GetStartInfo(string command) {
        if (OperatingSystem.IsWindows())
            return new ProcessStartInfo {
                FileName = "cmd.exe",
                Arguments = $"/c {command}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

        if (OperatingSystem.IsLinux())
            return new ProcessStartInfo {
                FileName = "/bin/bash",
                Arguments = $"-c \"{command}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };

        throw new PlatformNotSupportedException();
    }

    public void Run(string command) {
        var process = new Process();

        process.StartInfo = GetStartInfo(command);
        process.Start();
        process.WaitForExit();

        var output = process.StandardOutput.ReadToEnd();
        var error = process.StandardError.ReadToEnd();

        Console.WriteLine(output);
        Console.WriteLine(error);
    }
}