// using System.Diagnostics;
//
// var threads = Process.GetCurrentProcess().Threads;
//
// Console.WriteLine($"Total threads: {threads.Count}");
// foreach (ProcessThread thread in threads)
// {
//     Console.WriteLine($"Id: {thread.Id}\tThreadState: {thread.ThreadState}");
// }
//


using System.Runtime.InteropServices;

unsafe
{
    int length = 10;

    // Выделяем память в куче для массива из 10 элементов типа int
    int* arrayPtr = (int*)Marshal.AllocHGlobal(length * sizeof(int));

    try
    {
        // Инициализируем массив
        for (int i = 0; i < length; i++)
        {
            arrayPtr[i] = i + 1;
        }

        // Вывод значений массива
        Console.WriteLine("Array values in heap:");
        for (int i = 0; i < length; i++)
        {
            Console.WriteLine($"Element {i}: {arrayPtr[i]}");
        }
    }
    finally
    {
        // Освобождаем выделенную память
        Marshal.FreeHGlobal((IntPtr)arrayPtr);
    }
}

