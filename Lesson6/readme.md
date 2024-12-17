# Урок №6:
- switch 
- enum 
- all constructors 
- static constructor


## switch 

В принципе ничего нового в отличии от С++ вы не увидите, кроме как 
то что здесь нету fallthrough, и что можно использовать не только
int, но и string, enum, и т.д.

```csharp
int a = 5;

switch (a)
{
    case 1:
        Console.WriteLine("1");
        break;
    case 2:
        Console.WriteLine("2");
        break;
    case 3:
        Console.WriteLine("3");
        break;
    default:
        Console.WriteLine("default");
        break;
}
```

Вот пример с float
```csharp
float a = 5.0f;

switch (a)
{
    case 1.0f:
        Console.WriteLine("1");
        break;
    case 2.0f:
        Console.WriteLine("2");
        break;
    case 3.0f:
        Console.WriteLine("3");
        break;
    default:
        Console.WriteLine("default");
        break;
}
```

case с использованием нескольких значений
```csharp
int a = 5;

switch (a)
{
    case 1:
    case 2:
    case 3:
        Console.WriteLine("1, 2, 3");
        break;
    default:
        Console.WriteLine("default");
        break;
}
```

## ENUM 

```csharp
enum Days
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}

Days day = Days.Monday;

```

Отличий от С++ нету, кроме как того что в C# можно использовать 
любой тип данных, а не только int.

```csharp

enum Days : byte
{
    Monday,
    Tuesday,
    Wednesday,
    Thursday,
    Friday,
    Saturday,
    Sunday
}
```

или же вот enum с использованием строк

```csharp
enum Days
{
    Monday = 1,
    Tuesday = 2,
    Wednesday = 3,
    Thursday = 4,
    Friday = 5,
    Saturday = 6,
    Sunday = 7
}
```

Если сделать так 

```csharp

enum Days
{
    Monday = 7,
    Tuesday,
    Wednesday,
    Thursday,
    Friday ,
    Saturday,
    Sunday
}

```

то Tuesday будет равен 8, Wednesday 9 и т.д.

По умолчанию первый элемент равен 0, но можно изменить это поведение

## All constructors

В C# есть 5 типов конструкторов
1. Default constructor
2. Parameterized constructor
3. Copy constructor
4. Static constructor
5. Private constructor


### Default constructor

Конструктор по умолчанию создается автоматически, если вы не создали ни одного конструктора.
Если же вы создали хотя бы один конструктор, то конструктор по умолчанию не будет создан.

## Parameterized constructor

В новой версии C# 12 появилась возможность создавать конструктор сразу при объявлении класса
```csharp

Car c1 = new("BMW", "528i", 2002);

Console.WriteLine($"Make: {c1.Make}, Model: {c1.Model}, Year: {c1.Year}");

class Car(string make, string model, int year)
{
    public string Make { get; set; } = make;
    public string Model { get; set; } = model;
    public int Year { get; set; } = year;
}
```

В таком случае конструктор будет создан автоматически, и вам не нужно будет его создавать. НО НЕЛЬЗЯ СОЗДАТЬ КОНСТРУКТОР ПО УМОЛЧАНИЮ

## Copy constructor

```csharp
Car c1 = new("BMW", "528i", 2002);

Car c2 = new(c1);

Console.WriteLine($"Make: {c2.Make}, Model: {c2.Model}, Year: {c2.Year}");

class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    public Car(string make, string model, int year)
    {
        Make = make;
        Model = model;
        Year = year;
    }

    public Car(Car c)
    {
        Make = c.Make;
        Model = c.Model;
        Year = c.Year;
    }
}
```

## Static constructor

Статический конструктор вызывается только один раз, когда создается первый объект класса или обращаются к статическому члену класса.

Тут конечно же у вас может возникнуть много вопросов, но давайте для начала разберем отличия между статическими классом и не статическими.

1. Статический класс не может иметь экземпляров, а не статический класс может.
2. Статический класс не может иметь не статических членов, а не статический класс может иметь и те и другие.
3. Статический класс не может наследоваться, а не статический класс может.
4. Статический класс не может быть абстрактным, а не статический класс может.
5. Статический класс не может быть sealed, а не статический класс может.

Лично я когда изучал С# в далеком 2017 я задался вопросом. Если я могу в обычном классе создавать статичные члены, то 
смысл в статическом классе ?

Вот пример таких классов 

```csharp
class Car {
    public string Make { get; set; }
    public string Model { get; set; }
    
    public static int Count { get; set; }
    
    public Car(string make, string model) {
        Make = make;
        Model = model;
        Count++;
    }
    
    public static void PrintCount() {
        Console.WriteLine($"Count: {Count}");
    }
```

А в статическом классе соответственно только статические члены и ничего более. 

Теперь давайте повторим тот самый вопрос. Если я могу в обычном классе создавать статичные члены, то
смысл в статическом классе ?

Ответ прост. Статический класс не может иметь экземпляров, а не статический класс может.

Также хочу отметить то что говорил Максуд, правда это не ответ на данный вопрос, но все же.

Работа со статичными членами(не когда сам класс статичный, а когда члены статичные) означает ято они инициализируются только один раз, 
во время первого обращения к ним. Это может быть полезно, если у вас есть какие-то данные, которые не изменяются во время работы программы.

Теперь давайте вернемся к статическому конструктору. 

```csharp

class Car
{
    public string Make { get; set; }
    public string Model { get; set; }
    public int Year { get; set; }

    public static int Count { get; set; }

    static Car()
    {
        Count = 0;
    }

    public Car(string make, string model, int year)
    {
        Make = make;
        Model = model;
        Year = year;
        Count++;
    }

    public static void PrintCount()
    {
        Console.WriteLine($"Count: {Count}");
    }
}
```

Внимание вопрос, зачем мне статический конструктор, если я могу инициализировать статические члены в месте их объявления ?

Дело в том что если нам нужно поставить какое-то условие или мой статичный член привязан к другому статичному члену, то
нам нужно использовать статический конструктор.

1. Сложная логика инициализации
Если для инициализации статических членов требуется выполнить какую-либо сложную логику, например, вызов метода, выполнение вычислений или взаимодействие с внешними ресурсами, это невозможно сделать в месте объявления. Например:


```csharp

class Example
{
    public static readonly int Value;

    static Example()
    {
        // Сложная логика для инициализации
        Value = DateTime.Now.Year % 100; 
    }
}
```

2. Инициализация перед первым использованием

Статический конструктор вызывается только один раз, до первого обращения к любому члену типа (полям, методам, свойствам) или перед созданием экземпляра класса. Это гарантирует отложенную инициализацию только тогда, когда это действительно нужно.

```csharp
class Logger
{
    public static readonly string LogPath;

    static Logger()
    {
        LogPath = $"log_{DateTime.Now:yyyyMMdd}.txt";
        Console.WriteLine("Статический конструктор вызван");
    }
}

// Код
Console.WriteLine(Logger.LogPath); // Статический конструктор вызовется только сейчас

```

4. Выполнение кода перед доступом к типу

Если требуется выполнить какой-либо код до первого использования типа (например, запись в лог, настройка окружения), то статический конструктор идеально подходит.

```csharp
class Tracker
{
    static Tracker()
    {
        Console.WriteLine("Tracker initialized!");
    }

    public static void Track(string eventName)
    {
        Console.WriteLine($"Event: {eventName}");
    }
}

// Код
Tracker.Track("App Started"); // Выведет "Tracker initialized!" только при первом вызове
```

5. Контроль порядка инициализации

Если статический член зависит от значений других членов или сложной конфигурации, статический конструктор помогает точно контролировать порядок выполнения кода:

```csharp


class Dependency
{
public static string Config;

    static Dependency()
    {
        Config = "Initialized";
    }
}

class Example
{
public static string Value;

    static Example()
    {
        Value = Dependency.Config + " - Extended";
    }
}

```



