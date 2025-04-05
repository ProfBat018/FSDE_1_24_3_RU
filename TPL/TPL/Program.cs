#region Part1

/*
int counter = 0;

Console.WriteLine($"This is main thread: {Thread.CurrentThread.ManagedThreadId}");

Task.Run(() =>
{
    Console.WriteLine($"This is task from: {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine($"Is ThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");

    Console.WriteLine("This is non waiting task");
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine(++counter);
    }

    Console.WriteLine($"End of task: {Thread.CurrentThread.ManagedThreadId}");
}).Wait();


Task.Run(() =>
{
    Console.WriteLine($"This is task from: {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine($"Is ThreadPoolThread: {Thread.CurrentThread.IsThreadPoolThread}");

    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine(++counter);
    }

    Console.WriteLine($"End of task: {Thread.CurrentThread.ManagedThreadId}");
}).Wait();


Console.WriteLine($"This is end of Main Thread: {Thread.CurrentThread.ManagedThreadId}");
*/

/*
В данном контексте если не сделать Task.Wait(), то программа завершится до того, как выполнится Task.
Это связано с тем, что Task выполняется в пуле потоков, и если основной поток завершится, то все потоки в пуле будут завершены.
Важно помнить, что Task.Run() не блокирует основной поток, поэтому если вы хотите дождаться завершения задачи,
вам нужно использовать Task.Wait() или Task.Result.
*/

#endregion

#region Part2

/*
void findEven(IEnumerable<int> nums)
{
    Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine("Even numbers:");
    nums.Where(n => n % 2 == 0).ToList().ForEach(n => Console.Write($"{n} "));

    Console.WriteLine($"End of thread: {Thread.CurrentThread.ManagedThreadId}");
}

void findOdd(IEnumerable<int> nums)
{
    Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
    Console.WriteLine("Odd numbers:");
    nums.Where(n => n % 2 != 0).ToList().ForEach(n => Console.Write($"{n} "));

    Console.WriteLine($"End of thread: {Thread.CurrentThread.ManagedThreadId}");
}


int[] nums = { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

Task.Run(() => findEven(nums)).GetAwaiter().GetResult();
Task.Run(() => findOdd(nums)).GetAwaiter().GetResult();

// Thread.Sleep(1000);
*/

#endregion


#region Part3

//
// Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
//
// Task printNums(IEnumerable<int> nums)
// {
//     Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} is working on print");
//     foreach (var num in nums)
//     {
//         Console.Write($"{num} ");
//     }
//
//     Console.WriteLine();
//     Console.WriteLine($"End of thread: {Thread.CurrentThread.ManagedThreadId}");
//
//     return Task.CompletedTask;
// }
//
// Task<IEnumerable<int>> findEven(IEnumerable<int> nums)
// {
//     Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
//     return Task.FromResult(nums.Where(num => num % 2 == 0));
// }
//
// Task<IEnumerable<int>> findOdd(IEnumerable<int> nums)
// {
//     Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
//     return Task.FromResult(nums.Where(num => num % 2 != 0));
// }
//
//
// var nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
//
// var evenNums = Task.Run(() => findEven(nums));
// var oddNums = Task.Run(() => findOdd(nums));
//
// Task.Run(() => printNums(evenNums.GetAwaiter().GetResult()).ContinueWith(task => printNums(oddNums.GetAwaiter().GetResult())));
//
//
// Console.WriteLine($"End of thread: {Thread.CurrentThread.ManagedThreadId}");

#endregion


#region Part4

//
// Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
//
// Task printNums(IEnumerable<int> nums)
// {
//     return Task.Run(() =>
//     {
//         Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} is working on print");
//         foreach (var num in nums)
//         {
//             Console.Write($"{num} ");
//         }
//
//         Console.WriteLine();
//         Console.WriteLine($"End of thread: {Thread.CurrentThread.ManagedThreadId}");
//     });
// }
//
// Task<IEnumerable<int>> findEven(IEnumerable<int> nums)
// {
//     return Task.Run(() =>
//     {
//         Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
//
//         return nums.Where(num => num % 2 == 0);
//     });
// }
//
// Task<IEnumerable<int>> findOdd(IEnumerable<int> nums)
// {
//     return Task.Run(() =>
//     {
//         Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
//         Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
//         return nums.Where(num => num % 2 != 0);
//     });
// }
//
//
// var nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
//
// var evenNums = findEven(nums);
// var oddNums = findOdd(nums);
//
// Task.Run(() => printNums(evenNums.GetAwaiter().GetResult())).ContinueWith(task => printNums(oddNums.GetAwaiter().GetResult()));
//
// // Task.Run(() => printNums(evenNums.GetAwaiter().GetResult()));
// // Task.Run(() => printNums(oddNums.GetAwaiter().GetResult()));
//
// Console.WriteLine($"End of thread: {Thread.CurrentThread.ManagedThreadId}");

#endregion

#region Part5

Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");

void printNums(IEnumerable<int> nums)
{
    Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} is working on print");
    foreach (var num in nums)
    {
        Console.Write($"{num} ");
    }

    Console.WriteLine();
    Console.WriteLine($"End of thread: {Thread.CurrentThread.ManagedThreadId}");
}

async Task<IEnumerable<int>> findEven(IEnumerable<int> nums)
{
    return await Task.Run(() =>
    {
        Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");

        return nums.Where(num => num % 2 == 0);
    });
}

async Task<IEnumerable<int>> findOdd(IEnumerable<int> nums)
{
    return await Task.Run(() =>
    {
        Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
        return nums.Where(num => num % 2 != 0);
    });
}


var nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var evenNums = await findEven(nums);
var oddNums = await findOdd(nums);

printNums(evenNums);
printNums(oddNums);

Console.WriteLine($"End of thread: {Thread.CurrentThread.ManagedThreadId}");

#endregion