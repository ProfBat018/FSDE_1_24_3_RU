using System.Diagnostics;

#region Part1
// Console.WriteLine("Aloha");
//
// ProcessStartInfo startInfo = new ProcessStartInfo
// {
//     FileName = "/Users/wayne/Documents/Work/FSDE_1_24_3_RU/ConsoleApp1/bin/Debug/net9.0/ConsoleApp1", // Сам файл
//     UseShellExecute = false // Обязательно false, иначе MacOS не выполнит
// };
//
//
// Process.Start(startInfo);
//


#endregion

#region Part2

// вывод информации о потоке, в котором мы находимся 

// Console.WriteLine($"Id: {Thread.CurrentThread.ManagedThreadId}\n" +
//                   $"State: {Thread.CurrentThread.ThreadState}");


// ProcessThreadCollection threads = Process.GetCurrentProcess().Threads;
//
// Console.WriteLine($"Threads: {threads.Count}");
// foreach(ProcessThread thread in threads)
// {
//     Console.WriteLine($"Id: {thread.Id}\n" +
//                       $"State: {thread.ThreadState}");
// }

#endregion

#region Part3
//
// int number1 = 1, number2 = 2;
// int res = 0;
//
// Console.WriteLine($"This is Main thread with id: {Thread.CurrentThread.ManagedThreadId}");
//
// Thread th1 = new(() =>
// {
//     Console.WriteLine($"This is my thread");
//     Console.WriteLine($"ID: {Thread.CurrentThread.ManagedThreadId}");
//
//     res = number1 + number2;
//
// });
//
// th1.Start();
//
//
// Thread.Sleep(1000);
// Console.WriteLine($"Result: {res}");
// Console.WriteLine("End of Main");

#endregion

#region Part4


// Console.WriteLine($"This is Main thread with id: {Thread.CurrentThread.ManagedThreadId}");
//
// Thread th1 = new((param) =>
// {
//     Console.WriteLine($"This is my thread");
//     Console.WriteLine($"ID: {Thread.CurrentThread.ManagedThreadId}");
//
//     int[] numbers = (int[])param;
//
//     int res = 0;
//     foreach (var num in numbers)
//     {
//         res += num;
//     }
//     Console.WriteLine($"Result: {res}");
//
// });
//
// th1.Start(new int[] { 1, 2, 3, 4, 5 });
//
// Console.WriteLine("End of Main thread");



#endregion

#region Part5


Console.WriteLine("Start of Main...");

for(int i = 0; i < 10; i++)
{
    Console.WriteLine($"Thread {i} created");
    Thread th = new(() =>
    {
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} started");
        Thread.Sleep(100);
        Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} finished");
        
    });
    th.Start();
}

Thread.Sleep(1000);

Console.WriteLine("End of Main...");


#endregion

