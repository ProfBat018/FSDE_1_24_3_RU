# Тема урока: Структуры и Классы 

Сегодня мы с вами полностью закончим тему структур и классов.

Как мы уже с вами поняли данные в C# делятся на `Value Type` и `Reference Type`. Value Type - это типы данных, которые хранятся в стеке, а Reference Type - это типы данных, которые хранятся в куче.

Классы по умолчанию являются Reference Type, а структуры Value Type.

В C# есть свод правил работы с классами и структурами. Начнем с их различий:

Структуры:
1. Структуры являются Value Type.
2. Структуры не могут наследоваться от других структур или классов.
3. Структуры не могут иметь конструктор без параметров.
4. Структуры не могут иметь деструктор.
5. Структуры не могут быть абстрактными.
6. Вы не можете использовать инициализатор для поля без конструктора.

Классы:
1. Классы являются Reference Type.
2. Классы могут наследоваться от других классов.
3. Классы могут иметь конструктор без параметров.
4. Классы могут иметь деструктор.
5. Классы могут быть абстрактными.
6. Вы можете использовать инициализатор для поля без конструктора.

Вы вряд ли будете использовать структуры в своем коде, но все же важно знать о них, потому что вы используете уже готовые структуры в C#. Да и если вы будете работать с над библиотеками или фреймворками, то вам придется работать с структурами.

## Properties 

`Property` - это getter и setter для поля. 

```csharp
public class Person
{
    private string name;
    public string Name
    {
        get { return name; }
        set { name = value; }
    }
}
```

В примере выше, `Name` - это свойство, а `name` - это поле. Данное дуо называется property with backing field.

В C# есть сокращенная запись для свойств:

```csharp
public class Person
{
    public string Name { get; set; }
}
```

В примере выше, `Name` - это свойство, а `name` - это поле. Данное дуо называется auto-implemented property.

Тут инкапсуляция полкей отличается от С++, вы можете сказать что создавать приватное поле и публичные свойства для него это лишняя работа, но в C# в этом есть смысл. В будущем вы будете работать с такими фреймворками как `Entity Framework`, `AutoMapper`, который использует свойства для работы с данными. Также, в C# есть `event`(пройдем на следующих уроках), который работает только с свойствами. 


Свойствам можно задать модификаторы доступа, такие как `public`, `private`, `protected`, `internal`, `protected internal`. 

Так же, свойства могут быть только для чтения или только для записи:

```csharp

public class Person
{
    public string Name { get; } // Только для чтения
    public string Name { set; } // Только для записи
}
```

Можно спросить, как записать данные в свойство, которое только для чтения? Для этого используется конструктор:

```csharp

public class Person
{
    public string Name { get; } // Только для чтения
    public Person(string name)
    {
        Name = name;
    }
}
```

Или же, как считать данные из свойства, у которого нет `get`, Такие свойства называются `write-only` и могут использовать только внутри методов класса через `this`(не обязательно писать)

```csharp

public class Person
{
    public string Name { set; } // Только для записи
    public void PrintName()
    {
        Console.WriteLine(this.Name);
    }
}
```

`init` - это новый модификатор доступа, который появился в C# 9.0. Он позволяет установить значение свойства только в конструкторе или инициализаторе.

```csharp

public class Person
{
    public string Name { get; init; } // Только для записи

    public Person()
    {
    }
    public Person(string name)
    {
        Name = name;
    }
}
```

и используются так:

```csharp
Person person = new("John");
person.Name = "Doe"; // Ошибка
```

или же 

```csharp
Person person = new()
{
    Name = "John"
}

person.Name = "Doe"; // Ошибка
```

Разница между `init` и `readonly` в том, что `init` можно установить значение свойства в конструкторе или инициализаторе, а `readonly` можно установить значение только в конструкторе.

Вот пример с `readonly` полем:

```csharp

public class Person
{
    public readonly string Name; // это уже поле, а не свойство. readonly свойства не существует

    public Person(string name)
    {
        Name = name;
    }
}

var person = new Person("John");

person.Name = "Doe"; // Ошибка
```

Немного взрыв головы для вас, `readonly` - это по факту константа, которая может быть установлена только в конструкторе. Вы можете спросить, а для чего тогда `const` в этом языке ? Тут он делает совсем другую работу в отличии от С++. В C# ключевое слово `const` по факту можете считать `static readonly`, но с одним отличием, что `const` - это компилируемая константа, а `readonly` - это константа, которая устанавливается во время выполнения программы.

```csharp

public class Person
{
    public const string Name = "John"; 
}

var person = new Person();

person.Name = "Doe"; // Ошибка

Console.WriteLine(person.Name); // ошибка

Console.WriteLine(Person.Name); // John
```

## Indexers

`Indexer` - это свойство, которое позволяет обращаться к объекту, как к массиву. В С++ вы называли это перегрузкой оператора `[]`.

```csharp

public class Person
{
    private string[] names = new string[10]; // Массив из 10 имен
   
    public string this[int index] // Особый вид свойства, который называется индексатор
    {
        get { return names[index]; }
        set { names[index] = value; } // value - это ключевое слово, в которое автоматически передается значение, которое вы хотите установить
    }
}

var person = new Person();

person[0] = "John";

Console.WriteLine(person[0]);
```

`Indexer` может иметь несколько параметров:

```csharp

public class Person
{
    private string[] names = new string[10]; // Массив из 10 имен
   
    public string this[int index, int index2] // Особый вид свойства, который называется индексатор
    {
        get { return names[index]; }
        set { names[index] = value; } // value - это ключевое слово, в которое автоматически передается значение, которое вы хотите установить
    }
}

var person = new Person();

person[0, 0] = "John";




