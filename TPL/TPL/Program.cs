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

/*
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

async Task<IEnumerable<int>> findEvenAsync(IEnumerable<int> nums)
{
    return await Task.Run(() =>
    {
        Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");

        return nums.Where(num => num % 2 == 0);
    });
}

async Task<IEnumerable<int>> findOddAsync(IEnumerable<int> nums)
{
    return await Task.Run(() =>
    {
        Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
        Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
        return nums.Where(num => num % 2 != 0);
    });
}


var nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var evenNums = await findEvenAsync(nums);
var oddNums = await findOddAsync(nums);

printNums(evenNums);
printNums(oddNums);

Console.WriteLine($"End of thread: {Thread.CurrentThread.ManagedThreadId}");
*/

#endregion

#region Part6

// Console.WriteLine($"Start of main thread: {Thread.CurrentThread.ManagedThreadId}");
//
// void printNums(IEnumerable<int> nums)
// {
//     Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} is working on print");
//     foreach (var num in nums)
//     {
//         Console.Write($"{num} ");
//     }
//
//     Console.WriteLine();
//     Console.WriteLine($"End of thread: {Thread.CurrentThread.ManagedThreadId}");
// }
//
// IEnumerable<int> findEven(IEnumerable<int> nums)
// {
//     Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
//     return nums.Where(x => x % 2 == 0);
// }
//
// IEnumerable<int> findOdd(IEnumerable<int> nums)
// {
//     Console.WriteLine($"Start of thread: {Thread.CurrentThread.ManagedThreadId}");
//     return nums.Where(x => x % 2 != 0);
// }
//
// var nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
//
// var evenNums = Task.Run(() => findEven(nums)).GetAwaiter().GetResult();
// var oddNums = Task.Run(() => findOdd(nums)).GetAwaiter().GetResult();
//
// printNums(evenNums);
// printNums(oddNums);
//
// Console.WriteLine($"End of main Thread: {Thread.CurrentThread.ManagedThreadId}");

#endregion

#region Part7

/*
Console.WriteLine($"Start of main thread: {Thread.CurrentThread.ManagedThreadId}");

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

Task<IEnumerable<int>> findEven(IEnumerable<int> nums)
{
    Console.WriteLine($"Find even start with thread: {Thread.CurrentThread.ManagedThreadId}");
    return Task.Run(() =>
    {
        Console.WriteLine($"Find even task started with thread: {Thread.CurrentThread.ManagedThreadId}");
        return nums.Where(x => x % 2 == 0);
    });
}

Task<IEnumerable<int>> findOdd(IEnumerable<int> nums)
{
    Console.WriteLine($"Find odd start with thread: {Thread.CurrentThread.ManagedThreadId}");
    return Task.Run(() =>
    {
        Console.WriteLine($"Find odd task started with thread: {Thread.CurrentThread.ManagedThreadId}");
        return nums.Where(x => x % 2 != 0);
    });
}

var nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

// var evenNums = findEven(nums).GetAwaiter().GetResult();
// var oddNums = findOdd(nums).GetAwaiter().GetResult();

// Task.Run(() => printNums(evenNums)).ContinueWith(task => printNums(oddNums));

// printNums(evenNums);
// printNums(oddNums);

printNums(findEven(nums).GetAwaiter().GetResult());
printNums(findOdd(nums).GetAwaiter().GetResult());


Console.WriteLine($"End of main Thread: {Thread.CurrentThread.ManagedThreadId}");

*/
#endregion

#region Part8

/*
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

async Task<IEnumerable<int>> findEvenAsync(IEnumerable<int> nums)
{
    Console.WriteLine($"Find even start with thread: {Thread.CurrentThread.ManagedThreadId}");
    await Task.Delay(1000);
    return await Task.Run(() =>
    {
        Console.WriteLine($"Find even start with thread: {Thread.CurrentThread.ManagedThreadId}");
        IEnumerable<int> evenNums = nums.Where(x => x % 2 == 0);
        return evenNums;
    });
}

async Task<IEnumerable<int>> findOddAsync(IEnumerable<int> nums)
{
    Console.WriteLine($"Find odd start with thread: {Thread.CurrentThread.ManagedThreadId}");
    await Task.Delay(1000);
    return await Task.Run(() =>
    {
        Console.WriteLine($"Find odd start with thread: {Thread.CurrentThread.ManagedThreadId}");
        IEnumerable<int> oddNums = nums.Where(x => x % 2 != 0);
        return oddNums;
    });
}

var nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var evenNums = await findEvenAsync(nums);
var oddNums = await findOddAsync(nums);

printNums(evenNums);
printNums(oddNums);

*/

// printNums(await findEvenAsync(nums));
// printNums(await findOddAsync(nums));

// В отличии от кода сверху где каждый блок кода ждал пока выполнится другой в данном случае мы написали асинхронный код.
// Для того чтобы понять во что это все преобразовывается мы можем написать все вручную.

#endregion

/*
    Фишкой такого подхода является то, что мы используем Task, который использует ThreadPool.
    Наш ThreadPool может быть даже не выделит новый поток, а просто переназначит действие старого.
    Таким образом мы написали многопоточный код без явного использования Thread.

    Это делает код более легковесным и управляемым, так как не нужно заботиться о создании и завершении потоков вручную.
    Task.Run под капотом использует пул потоков, что означает:

    - Нет необходимости вручную управлять жизненным циклом потоков.
    - Потоки переиспользуются, а значит — меньше накладных расходов на создание/уничтожение.
    - Мы можем запускать несколько задач параллельно, не блокируя основной поток.
    - Такой подход хорошо масштабируется при умеренной нагрузке и идеально подходит для CPU-bound задач (как фильтрация).

    Однако важно понимать, что Task.Run не делает код "магически" асинхронным — это просто удобная обёртка для выполнения действий в другом потоке.
    Поэтому если задача не требует тяжелой работы CPU, а, например, работает с I/O (файлы, БД, сеть), лучше использовать настоящие async-методы с await.

    Также стоит быть осторожным при использовании GetAwaiter().GetResult() или .Result — они блокируют поток и **могут привести к deadlock'ам в UI-приложениях** (например, в WPF или WinForms), если не использовать их правильно.

    ✅ В консольных и серверных приложениях (например, ASP.NET Core) такой подход безопасен при контролируемом использовании.

    Вывод: мы избавились от `async/await`, сохранили асинхронность, сделали код многопоточным и понятным, используя `Task` и `ThreadPool` напрямую.
   */

#region Part9

// Parallel .ForEach

/*
 Класс Parallel позволяет выполнять параллельные операции над коллекциями. 
    Например, Parallel.ForEach позволяет выполнять итерации по элементам коллекции в параллельных потоках.
 */
/*
var nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

Parallel.ForEach(nums, num =>
{
    Console.WriteLine($"Thread: {Thread.CurrentThread.ManagedThreadId} is working on {num}");
    Console.WriteLine($"End of thread: {Thread.CurrentThread.ManagedThreadId} is working on {num}");
});
*/
// Такой подход нужен если нам не важке порядок выполнения задач, а важна скорость выполнения. 

// Parallel LINQ 

// Parallel LINQ (PLINQ) позволяет выполнять LINQ-запросы параллельно, что может значительно ускорить выполнение запросов на больших объемах данных.






#endregion


#region PLINQ_TEST

using System.Diagnostics;

Stopwatch stopwatch = new Stopwatch();

stopwatch.Start();
var nums = new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

var evenNums = nums.AsParallel().Where(x => x % 2 == 0).ToList();

stopwatch.Stop();

Console.WriteLine(
    $"Time taken for LINQ: {stopwatch.ElapsedTicks} ms");

stopwatch.Reset();




#endregion