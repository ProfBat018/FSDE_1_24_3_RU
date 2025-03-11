using System.Diagnostics;


Console.WriteLine($"Process with id: {Process.GetCurrentProcess().Id} started");
string childApp =
    "/Users/wayne/Documents/Work/FSDE_1_24_3_RU/Processes/ChildProcessExample/bin/Debug/net9.0/ChildProcessExample"; // Путь к исполняемому файлу

ProcessStartInfo psi = new ProcessStartInfo
{
    FileName = childApp,
    Arguments = "arg1 arg2 arg3",
    RedirectStandardOutput = true
};

using Process process = new Process { StartInfo = psi };

process.EnableRaisingEvents = true;
process.Exited += (sender, e) => Console.WriteLine($"Child process with id: {process.Id} finished");
process.OutputDataReceived += (sender, e) => Console.WriteLine(e.Data);

process.Start();

Console.WriteLine($"Child process with id: {process.Id} started");

process.BeginOutputReadLine();

process.WaitForExit();


Console.WriteLine($"Process with id: {Process.GetCurrentProcess().Id} finished");