# Урок № 7
- abstract class
- overridable methods
- abstract and virtual properties
- constructor chaining
- what's new in C# 10, 11, 12, 13

## Abstract class

Абстрактные классы - это классы чей объект нельзя создать. Если в С++ нельзя было явно объявить такой класс, то здесь 
такое ключевое слово есть. Давайте разберем на примере и посмотрим как это работает.

```csharp

abstract class Animal 
{
    public abstract void Voice();
}

class Dog : Animal
{
    public override void Voice()
    {
        Console.WriteLine("Gav");
    }
}

class Cat : Animal
{
    public override void Voice()
    {
        Console.WriteLine("Meow");
    }
}
```

Напоминаю, что в C# нет функции множественного наследования. Любой класс по умолчанию наследуется от класса Object и по
факту у вас остается еще один класс для наследования. В данном случае это класс Animal.

В реальных примерах использования если 