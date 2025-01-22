#region Part1




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
#endregion

#region Part2

/*
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
*/
#endregion

#region Part4 

// String copy
/*
string a = "Elvin";

string b = a;

Console.WriteLine(a);
Console.WriteLine(b);

a = "Azimov";

Console.WriteLine(a);
Console.WriteLine(b);
*/


// shallow copy with class

/*
Person person1 = new Person { Name = "Elvin" };

Person person2 = person1;

Console.WriteLine(person1.Name);

Console.WriteLine(person2.Name);

person1.Name = "Azimov";

Console.WriteLine(person1.Name);

Console.WriteLine(person2.Name);

class Person
{
    public string Name { get; set; }
}
*/

#endregion


string name = "Elvin";

while (true)
{
    Console.WriteLine(name);
    name = name + "Elvin";
}

