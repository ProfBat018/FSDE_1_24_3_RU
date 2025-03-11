using System;
using System.Diagnostics;


Console.WriteLine("Дочерний процесс запущен!");
foreach (var arg in args)
{
    Console.WriteLine($"Получен аргумент: {arg}");
}


