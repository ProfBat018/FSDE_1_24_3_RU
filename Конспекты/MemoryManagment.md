# Введение в управление памятью

Как мы уже с вами знаем, `.NET` - это экосистема которая может очень умным образом управлять как процессорным временем, так и памятью. Для того чтобы понять как работает память в `.NET` нам нужно понимать что такое `Managed` и `Unmanaged` память.

Память состоит из двух частей:

- Stack
- Heap

Со `Stack`-ом вы уже знакомы еще со времен С++, в этой области хранится все, начиная от переменных, заканчивая информацией о вызове методов. `Stack` - это область памяти, которая выделяется во время компиляции программы. Раньше вы думали что он один у нас в программе, но на самом деле это не так.

В C# при запуске программы создается `Main` метод в классе `Program` и работает это все в `Main Thread`. Помимо `Main Thread` у нас создается еще несколько потоков в зависимости от типа проекта и его размера. Вот пример выводе информации о потоках в обычной консольной программе:

```csharp
using System.Diagnostics;

var threads = Process.GetCurrentProcess().Threads;

Console.WriteLine($"Total threads: {threads.Count}");
foreach (ProcessThread thread in threads)
{
    Console.WriteLine($"Id: {thread.Id}\tThreadState: {thread.ThreadState}");
}

```

Вот пример вывода:

```bash
Total threads: 11
Id: -311686880  ThreadState: Running
Id: 1837002976  ThreadState: Standby
Id: 1837576416  ThreadState: Standby
Id: 1838149856  ThreadState: Standby
Id: 1838723296  ThreadState: Standby
Id: 1839296736  ThreadState: Standby
Id: 1839870176  ThreadState: Standby
Id: 1840443616  ThreadState: Standby
Id: 1841017056  ThreadState: Running
Id: 1841328352  ThreadState: Standby
Id: 1841901792  ThreadState: Standby

```

Как вы видите у нас есть 11 потоков, один из которых `Main Thread`, а остальные потоки создаются в зависимости от того что мы делаем в программе.

`Stack` каждого потока весит 1 мегабайт, и если у вас есть 10 потоков, то у вас уже 10 мегабайт памяти занято.

Давайте поговорим подробно про `Managed` и `Unmanaged` память.

`Managed` память - это память которая управляется сборщиком мусора. Сборщик мусора - это программа которая следит за тем чтобы память была освобождена вовремя.

`Unmanaged` память - это память которая управляется программистом. За это отвечает ключевое слово `unsafe`(работала с указатеялями) и работа с сетевыми сокетами.

```csharp

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

```


