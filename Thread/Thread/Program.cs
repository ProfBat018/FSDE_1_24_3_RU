#region Part1

// Console.WriteLine("This is Main thread");
//
// void foo()
// {
//     Thread th1 = new(() =>
//     {
//         Console.WriteLine($"This is thread {Thread.CurrentThread.ManagedThreadId}");
//
//         for (int i = 0; i < 10; i++)
//         {
//             Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}");   
//         }
//     });
//
//     th1.Start();
//     
//     th1.Join(); // Wait for thread to finish
// }
//
// void foo2()
// {
//     Thread th2 = new(() =>
//     {
//         Console.WriteLine($"This is thread {Thread.CurrentThread.ManagedThreadId}");
//
//         for (int i = 0; i < 10; i++)
//         {
//             Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}");   
//         }
//     });
//
//     th2.Start();
// }
//
//
// foo();
// foo2();
//
// Thread.Sleep(100);
//
// Console.WriteLine("Main thread is done");
//
//
//

#endregion


#region Part2

/*
Console.WriteLine("This is Main thread");

int counter = 0;

Thread th1 = new(() =>
{
    Console.WriteLine($"This is thread {Thread.CurrentThread.ManagedThreadId}");

    int i = 0;
    while (i != 5)
    {
        i++;
        counter++;

        Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}\t" +
                          $"Counter = {counter}");
    }
});

th1.Start();

Thread th2 = new(() =>
{
    Console.WriteLine($"This is thread {Thread.CurrentThread.ManagedThreadId}");

    int i = 0;
    while (i != 5)
    {
        i++;
        counter++;

        Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}\t" +
                          $"Counter = {counter}");
    }
});

th2.Start();

Thread.Sleep(100);

Console.WriteLine("Main thread is done");

*/

#endregion

#region Part3

/*


Console.WriteLine("This is Main thread");

object lockObject = new();

int counter = 0;
int counter2 = 0;

Thread th3 = new(() =>
{
    Console.WriteLine($"This is thread {Thread.CurrentThread.ManagedThreadId}");

    int i = 0;
    while (i != 5)
    {
        i++;
        counter2++;

        Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}\t" +
                          $"Counter = {counter2}");
    }
});

th3.Start();

Thread th1 = new(() =>
{
    try
    {
        Monitor.Enter(lockObject);

        Console.WriteLine($"This is thread {Thread.CurrentThread.ManagedThreadId}");

        int i = 0;
        while (i != 5)
        {
            i++;
            counter++;

            Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}\t" +
                              $"Counter = {counter}");
        }
    }
    finally
    {
        Monitor.Exit(lockObject);
    }
});

th1.Start();

Thread th2 = new(() =>
{
    try
    {
        Monitor.Enter(lockObject);

        Console.WriteLine($"This is thread {Thread.CurrentThread.ManagedThreadId}");

        int i = 0;
        while (i != 5)
        {
            i++;
            counter++;

            Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}\t" +
                              $"Counter = {counter}");
        }
    }
    finally
    {
        Monitor.Exit(lockObject);
    }
});

th2.Start();

Thread.Sleep(100);

Console.WriteLine("Main thread is done");
*/

#endregion

#region Part4
/*
int counter = 0;
object lockObject = new();


Thread th1 = new(() =>
{
    lock (lockObject)
    {
        Console.WriteLine($"This is thread {Thread.CurrentThread.ManagedThreadId}");

        int i = 0;
        while (i != 5)
        {
            i++;
            counter++;

            Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}\t" +
                              $"Counter = {counter}");
        }
    }
});

th1.Start();

Thread th2 = new(() =>
{
    lock (lockObject)
    {
        Console.WriteLine($"This is thread {Thread.CurrentThread.ManagedThreadId}");

        int i = 0;
        while (i != 5)
        {
            i++;
            counter++;

            Console.WriteLine($"{i} from thread {Thread.CurrentThread.ManagedThreadId}\t" +
                              $"Counter = {counter}");
        }
    }
});

th2.Start();

Thread.Sleep(100);

Console.WriteLine("Main thread is done");

*/

#endregion

#region Part5
using System;
using System.Threading;

class Program
{
    static int highCount = 0, normalCount = 0, lowCount = 0;
    static bool stopFlag = false;

    static void Main()
    {
        Thread highPriorityThread = new Thread(IncrementHigh);
        Thread normalPriorityThread = new Thread(IncrementNormal);
        Thread lowPriorityThread = new Thread(IncrementLow);

        highPriorityThread.Priority = ThreadPriority.Highest;
        normalPriorityThread.Priority = ThreadPriority.Normal;
        lowPriorityThread.Priority = ThreadPriority.Lowest;

        highPriorityThread.Start();
        normalPriorityThread.Start();
        lowPriorityThread.Start();

        // Даем потокам 3 секунды на выполнение
        Thread.Sleep(3000);
        stopFlag = true;

        highPriorityThread.Join();
        normalPriorityThread.Join();
        lowPriorityThread.Join();

        Console.WriteLine("\nРезультаты:");
        
        int[] counts = { highCount, normalCount, lowCount };
        var res = counts.Max();

        
        Console.WriteLine($"Высокий приоритет: {highCount}");
        Console.WriteLine($"Обычный приоритет: {normalCount}");
        Console.WriteLine($"Низкий приоритет: {lowCount}");
        
        Console.WriteLine($"Наибольшее количество итераций: {res}");
    }

    static void IncrementHigh()
    {
        while (!stopFlag)
        {
            highCount++;
        }
    }

    static void IncrementNormal()
    {
        while (!stopFlag)
        {
            normalCount++;
        }
    }

    static void IncrementLow()
    {
        while (!stopFlag)
        {
            lowCount++;
        }
    }
}
#endregion

