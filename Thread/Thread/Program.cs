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

object lockObject = new();
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

Console.WriteLine("This is Main thread");

object lockObject = new();
int counter = 0;

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

#endregion