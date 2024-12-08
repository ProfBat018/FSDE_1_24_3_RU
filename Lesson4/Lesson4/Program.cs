using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Xml;

// namespace Lesson4;

#region ARGS

//
// public class Program
// {
//     public static void Main(string[] args)
//     {
//         Console.WriteLine("Start of program");
//         foreach (var item in args)
//         {
//             Console.WriteLine(item);
//         }
//
//         Console.WriteLine("End of Program");
//     }
// }

#endregion

#region StructVsClass

/*
// Readme про эту тему ищите в прошлом уроке. 

class ClassTest
{
    public int number = 5;
}

struct StructTest
{
    public StructTest()
    {
    }

    // Если вы делаете инициализацию полей, то обязаны создать конструктор
    public int number = 5;
}

class TestNumber
{
    public static void ChangeNum(ClassTest obj)
    {
        obj.number = 42;
    }

    public static void ChangeNum(StructTest obj) // Можно поставить ref 
    {
        obj.number = 42;
    }
}

class Program
{
    public static void Main()
    {
        ClassTest ct = new(); // Heap
        StructTest st = new(); // Stack

        Console.WriteLine($"Number before changing in Class: {ct.number}");
        TestNumber.ChangeNum(ct);
        Console.WriteLine($"Number after changing in Class: {ct.number}");

        Console.WriteLine("______________________________________________");


        Console.WriteLine($"Number before changing in Class: {st.number}");
        TestNumber.ChangeNum(st);
        Console.WriteLine($"Number after changing in Class: {st.number}");
    }
}

*/

#endregion

#region AnonymousTypesAndObjects

// Создание переменной 
/*
class Program
{
    public static void Main(string[] args)
    {
        var name4 = "Elvin"; // Анонимный тип данных
        string name = "Elvin";
        String name2 = new("Elvin");
        String name3 = new String("Elvin");
        
        
        //// Part 2
        

        // dynamic a = "Elvin";
        // Console.WriteLine($"Value: {a}, Data Type: {a.GetType()}");
        // a = 5;
        // Console.WriteLine($"Value: {a}, Data Type: {a.GetType()}");
        
    }
    
    
}

*/

#endregion

#region Boxing&Unboxing

/*
// Такой тип создания массивов вышел в .NET8 
int[] arr =  { 1, 2, 3, 4, 5 };

// Запись числовых значений в object - это boxing 
ArrayList arr2 = new (){ 1, "2", 3, 4, 5 };

int number = (int)arr2[0]; // Unboxing

Console.WriteLine(number);

// Решение проблемы => Generics(обобщения или шаблоны)

List<int> arr3 = new() { 1, 2, 3, 4, 5 };

*/
#endregion

#region Nullable

//// Можно сделать 
// string name = null;
//// Если ваш объект потенциально может принимать null, то вы должны сделать его nullable

// string? name = null;

//// Если вы пишете ? после типа, то вы объявляете его nullable и теперь обязаны проверять его на
//// null. Если вы это не сделаете, ошибки не будет, но это неправильно.

//
// Console.WriteLine("Enter your name: ");
//
// name = Console.ReadLine();
//
// if (name == null)
// {
//     Console.WriteLine("Name is null");
// }



#endregion

#region NullColeascing

//// NullColeascing operators - это операторы на проверку на null. 

// string? name = "Samir";
//
// Console.WriteLine(name ?? "Elvin");


#endregion


#region Classes

Car c1 = new()
{
    Make = "BMW",
    Model = "530XI"
};

class Car
{
    private string _color;
    public string Make { get; set; }
    public string Model { get; set; }

    public string Color
    {
        get => _color;
        set => _color = value;
    }
}


#endregion


