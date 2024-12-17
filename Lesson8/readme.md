# Урок №8

- nameof
- string
- StringBuilder
- Перегрузка операторов
- делегирование конструкторов

## nameof

`nameof` - оператор, который возвращает строку, содержащую имя переменной,
типа или члена.

```csharp

string name = "Elvin";

Console.WriteLine(nameof(name)); // name

```

Может быть полезно, когда мы хотим использовать имя переменной в качестве ключа
в словаре, например. Или же в будущем мы с вами пройдем рефлексию, где
`nameof` будет очень полезен.

## string

Вы уже знакомы с данным типом еще со времен `Python`, но в целом самое главное
что вы должны знать это базовый метод `ToString()`, который находится в классе `Object`.

Так же очень важно знать, что в классе String есть очень много полезных методов.

[Вот их список](https://learn.microsoft.com/en-us/dotnet/api/system.string.clone?view=net-9.0)

Ссылка сверху - это официальная документация, где вы можете почитать про каждый метод. С левой
панели вы увидите полный список всех методов, которые есть в классе `String`.

Вообще на `MSDN`, вы можете найти документацию по любому классу, который есть в `C#`. В отличии
от других языков, документация в `C#` очень хорошо написана и поддерживается на многих языках.

Объснять эти методы я вам не буду, читайте и смотрите сами, если что-то непонятно, то спрашивайте на занятии.

## StringBuilder

Давайте на секунду вспомним каким типом данных является `string`. Он ссылочный тип, который в принципе упрощает
и усложняет нам жизнь одновременно.

Простота заключается в том что в куче полно места и мы без проблем можем создавать строки в динамичном режиме.
Проблема заключается в том, что каждый раз когда мы создаем строку и изменяем ее, то создается новый объект в куче.

Важный момент про строчные литералы. Если у вас повторяются литералы, то CLR создает их в одном месте и
все ваши `instance` будут ссылаться на один и тот же объект.

```csharp

string a = "Hello";

Сonsole.WriteLine("Hello");

```

В данном случае `a` и `"Hello"` будут ссылаться на один и тот же объект.

Теперь давайте представим что происходит при обычной конкатенации строк.

```csharp

string name = "Elvin";

string surname = "Azimov";

string fullName = name + " " + surname;

```

В данном случае происходит следующее:

stack-name ===> heap-Elvin(10 bytes)

stack-surname ===> heap-Azimov(12 bytes)

stack-fullName ===> heap-Elvin Azimov(24 bytes)

Тут ничего особенного, но что если мы поменяем имя

```csharp

string name = "Elvin";

string surname = "Azimov";

string fullName = name + " " + surname;

Console.WriteLine(name);
Console.WriteLine(fullName);

name = name + "chik";

Console.WriteLine(name);
Console.WriteLine(fullName);
```

stack-name ===> heap-Elvin(10 bytes)

stack-surname ===> heap-Azimov(12 bytes)

stack-fullName ===> heap-Elvin Azimov(24 bytes)

stack-name ===> heap-Elvinchik(12 bytes)

![Принцип работы обычной строки](Screenshot%202024-12-17%20at%2020.04.39.png)

## StringBuilder

В случае с ним можно вспомнить обычный принцип работы строки в С++, где память вылелялась по 16*2
байтов каждый раз, когда строка увеличивалась. Тут тоже самое происходит.

У него есть 6 конструкторов.

1. StringBuilder()
2. StringBuilder(int capacity). Можно задать начальный размер строки
3. StringBuilder(string value). Можно задать начальное значение строки
4. StringBuilder(string value, int capacity). Можно задать начальное значение строки и начальный размер строки
5. StringBuilder(string value, int startIndex, int length, int capacity). Можно задать начальное значение строки,
   начальный размер строки и диапазон
6. StringBuilder(string value, int startIndex, int length, int capacity, int maxCapacity). Можно задать начальное
   значение строки, начальный размер строки, диапазон и максимальный размер строки

```csharp

StringBuilder sb = new(); // Работа по умолчанию, 32 байта
StringBuilder sb1 = new(10); // 20 байтов
StringBuilder sb2 = new("Hello"); // Hello и в остатке 22 байтов
StringBuilder sb3 = new("Hello", 10); // Hello и в остатке 10 байтов, так как выделил 20
StringBuilder sb4 = new("Hello", 0, 3, 10);
StringBuilder sb5 = new("Hello", 0, 3, 10, 100);

```

## Перегрузка операторов.

Сразу напомню вам один из наших предыдущих уроков, где мы рассматривали индексторы. Так вот,
индексаторы - это не перегрузка операторов, они считаются особым типом свойств.

Перегрузка операторов - это когда мы можем переопределить стандартное поведение операторов в `C#`.

Вот список операторов, которые можно перегружать:

- unary operators
- binary operators
- comparison operators
- conversion operators
- increment and decrement operators
- conditional logical operators
- equality operators
- bitwise logical operators
- shift operators

Все показывать не буду, приведу один пример.

```csharp

public class Complex
{
    // Пример комплексного числа = 1 + 2i
    // (1 + 2i) + (3 + 4i) = 4 + 6i
    public double Real { get; set; }
    public double Imaginary { get; set; }

    public static Complex operator +(Complex a, Complex b)
    {
        return new Complex(a.Real + b.Real, a.Imaginary + b.Imaginary);
    }
}
```

Если что `Complex` - это класс, который представляет комплексное число.

## Делегирование конструкторов

Делегирование - значит передача обязанностей. Я забыл показать вам его когда показывал наследование, 
так что вот вам пример с классами Car и Transportş 

```csharp

class Transport 
{
    public string Name { get; set; }
    public int Year { get; set; }

    public Transport(string name, int year)
    {
        Name = name;
        Year = year;
    }
}

class Car : Transport
{
    public string Model { get; set; }

    public Car(string name, int year, string model) : base(name, year)
    {
        Model = model;
    }
}

```

Как видите, в C# делегирование происходит с помощью ключевого слова `base`. В данном случае мы делегировали
конструктор класса `Transport` в класс `Car`.




