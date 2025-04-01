#region Part1

Console.WriteLine("Start of main thread");

var counter = 0;
object? lockObject = new();

void foo(string name)
{
    Console.WriteLine(name);
    lock (lockObject)
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(
                $"{name}, IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}, Counter: {counter++}");
        }
    }
}

ThreadPool.QueueUserWorkItem(_ => foo("Thread 1"));


var th1 = new Thread(() =>
{
    lock (lockObject)
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine(
                $"Thread 1, IsThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}, Counter: {counter++}");
        }
    }
});

th1.Start();

th1.Join();

Console.WriteLine("End of main thread");

#endregion