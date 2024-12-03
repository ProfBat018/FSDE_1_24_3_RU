#region БазовыйВвод

// Console.WriteLine("Enter your name: ");
// string name = Console.ReadLine();
//
//
// Console.WriteLine("Enter your surname: ");
// string surname = Console.ReadLine();
//
// Console.WriteLine("Enter your age: ");

// int age = Convert.ToInt32(Console.ReadLine()); // Плохой 
// int age = Int32.Parse(Console.ReadLine()); // Тоже плохой 

// int age;
// bool res = Int32.TryParse(Console.ReadLine(), out age); // Бомба 
//
// string consoleRes = res ? $"Your full name is: {name} {surname}, and you are at age: {age}" : "Error";
// Console.WriteLine(consoleRes);

#endregion

#region Отступление_К_Ссылкам

// void addTo(int number1, int number2)
// {
//     number1 += number2;
// }
//
//
// int n1 = 5;
// int n2 = 6;
//
// addTo(n1, n2);
//
// Console.WriteLine(n1);


// void addTo(ref int number1, ref int number2)
// {
//     number1 += number2;
// }
//
//
// int n1 = 5;
// int n2 = 6;
//
// addTo(ref n1, ref n2);
//
// Console.WriteLine(n1);


// void addTo(out int number1, out int number2)
// {
//     number1 = Int32.Parse(Console.ReadLine());
//     number2 = Int32.Parse(Console.ReadLine());
//
//     number1 += number2;
// }
//
//
// int n1;
// int n2;
//
// addTo(out n1, out n2);
//
// Console.WriteLine($"{n1}{n2}");

#endregion

#region Массивы

#region Одномерные

// int[] arr = new[] { 1, 2, 3, 4, 5 };
// int[] arr2 = new int[3] { 1, 2, 3 };
// int[] arr3 = { 1, 2, 3 };

#endregion

#region Многомерные

// Jagged Array

// int[][] arr1 = new int[2][]
// {
//     new int[3] { 1, 2, 3 },
//     new int[2] { 3, 4 }
// };

// Multidimensional array [3D]

// int[,,] arr2 = new int[,,]
// {
//     {
//         { 1, 2 },
//         { 3, 4 }
//     },
//     {
//         { 5, 6 },
//         { 7, 8 }
//     }
// };

// Multidimensional array [2D]

// int[,] arr3 = new int[,]
// {
//     { 1, 2 },
//     { 3, 4 }
// };

#endregion

#region ArrayList

// using System.Collections;

// ArrayList arr = new() {1, "Aloha", true, 3.2f, 3.3};

// int number1 = arr[0];

#endregion

#endregion

#region Классы

// Object a = new Ayxan();
//
// class Ayxan
// {
//     public string AlarmTone { get; set; }    
// }


#endregion

#region Структуры

using System.Reflection;


Console.WriteLine(typeof(Faxri).BaseType.Name);
A z = new C();

struct Faxri
{
    public bool isRight { get; set; }
}

class A
{
    
}

class B : A
{
    
}

class C : B
{
    
}


#endregion