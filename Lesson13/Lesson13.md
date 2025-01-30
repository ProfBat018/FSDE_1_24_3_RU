# Тема урока: Обобщения(Generics)

- Проблема в начале С#(Пример с ArrayList)
- Синтаксис
- Ограничения
- in, out
- Ковариантность и контрвариантность
- Обобщенные методы

В принципе вы уже более менее знакомы с этой темой из С++,
там вы называли их шаблонами. В C# они называются обобщениями.
На самом деле для высокоуровневых строго типизированных языков
это очень важная тема. В целом как я уже не раз говорил, что-бы
освоить какую-то тему нужно понять какую проблему она решает.
Так вот обобщения решают проблему работы с object и приведениями типов.
Также они позволяют создавать универсальные алгоритмы и структуры данных.
Эта тема настолько въелась в ООП, что в строго типизированных языках она
является ее частью.

Обобщения позволяют создавать классы, интерфейсы, методы и делегаты, события.

## Пример с ArrayList

Еще давным-давно, когда трава была зеленее, а солнце ярче,
в C# был такой класс ArrayList. Он есть и сейчас, но им никто не пользуется.

Дело в том, что ArrayList хранит элементы типа object. Если записать туда,
например int, то при чтении его нужно будет привести к int.

```csharp

ArrayList list = new ArrayList() { 1, 2, 3, 4, 5 };
int a = (int)list[0];
```

Такое приводит к boxing/unboxing, что не есть хорошо. Даже если вы не
будете делать boxing/unboxing, то все равно придется делать
приведение типов. Такой подход берет ресурсы.

Для решения этой проблемы и были введены обобщения.

## Синтаксис

Синтаксис обобщений очень простой. Вместо object пишем T(или любую другую букву).

```csharp



interface IProduct
{
    public string Make { get; set; }
    public string Model { get; set; }
}

class Laptop : IProduct
{
    public string Make { get; set; }
    public string Model { get; set; }
    public string Processor { get; set; }
    public int Ram { get; set; }
}

class MobilePhone : IProduct
{
    public string Make { get; set; }
    public string Model { get; set; }
    public string Processor { get; set; }
    public int Ram { get; set; }
    public int SimCount { get; set; }
}

class ElectricShop 
{
    public List<IProduct> Products { get; set; }
    
    public ElectricShop()
    {
        Products = new List<IProduct>() {new Laptop(), new MobilePhone()};        
    }
}


class Program
{
    static void Main()
    {
        ElectricShop shop = new ElectricShop();
        foreach (var product in shop.Products)
        {
            if (product is Laptop)
            {
                Laptop? laptop = product as Laptop;
                Console.WriteLine($"Laptop: {laptop.Make} {laptop.Model} {laptop.Processor} {laptop.Ram}");
            }
            else if (product is MobilePhone)
            {
                MobilePhone mobilePhone = (MobilePhone)product;
                Console.WriteLine(
                    $"MobilePhone: {mobilePhone.Make} {mobilePhone.Model} {mobilePhone.Processor} {mobilePhone.Ram} {mobilePhone.SimCount}");
            }
        }
    }
}
```

Но это не единственная причина по которой обобщения важны.
Они позволяют создавать универсальные алгоритмы и структуры данных.

Перед этим чуть-чуть поговорим о делегатах. 

```csharp
class Calculator
{
    public float CalculateOperation(Operation operation, float a, float b)
    {
        return operation(a, b);
    }
}

class Program
{
    static float Add(float a, float b)
    {
        return a + b;
    }
    
    static float Subtract(float a, float b)
    {
        return a - b;
    }
    static void Main(string[] args)
    {
        Calculator calculator = new();

        calculator.CalculateOperation(Add, 5, 3);

    }
}

public delegate float Operation(float a, float b);
```

```csharp
 internal class LoggerMiddleware : IMiddleware
    {
        public HttpHandler? Next { get; set; }

        public void Handle(HttpListenerContext context)
        {
            Console.WriteLine(context.Request.UserHostAddress);

            Next?.Invoke(context);
        }
    }
    
  internal class StaticFilesMiddleware : IMiddleware
    {
        public HttpHandler? Next { get; set; }

        public void Handle(HttpListenerContext context)
        {
            string url = context.Request.RawUrl;
            Console.WriteLine(url);

            Next?.Invoke(context);
        }
    }
    
  internal class MVCMiddleware : IMiddleware
    {
        public HttpHandler? Next { get; set; }

        public void Handle(HttpListenerContext context)
        {
            string url = context.Request.RawUrl;
            Console.WriteLine(url);

            Next?.Invoke(context);
        }
    }
```

```csharp

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebServer.Middlewares.Interfaces;

namespace WebServer.Middlewares.Classes
{
    public class MiddlewareBuilder
    {
        // Type - class, который хранит информацию о типе 
        // По сути в моем Stack будет храниться информация о типах:
        // WebServer.Middlewares.Classes.LoggerMiddleware
        // WebServer.Middlewares.Classes.StaticFilesMiddleware
        // WebServer.Middlewares.Classes.MVCMiddleware
        
        private Stack<Type> middlewares = new(); // MVCMiddleware LoggerMiddleware StaticFilesMiddleware 
        
        // 
        public void Use<T>() where T : IMiddleware
        {
            middlewares.Push(typeof(T));
        }

     
 
        public HttpHandler Build()
        {
            // context.Response.Close() - последний middleware, который закрывает соединение
            HttpHandler handler = context => context.Response.Close();

            while (middlewares.Count != 0)
            {
                Type type = middlewares.Pop();

                var middleware = Activator.CreateInstance(type) as IMiddleware;

                middleware.Next = handler;
                handler = middleware.Handle;
            }
            return handler;
        }
    }
}
```

## In, out

Данные ключевые слова используются для ограничения типов, в методах мы можем использовать
как переменные для ввода данных, так и для вывода. Самый лучший пример это встроенный делегаты
Func и Action.

Вот как выглядит делегат Func:





