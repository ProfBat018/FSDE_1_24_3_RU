# Встроенные интерфейсы в С#

    1.	IEnumerable

Представляет коллекцию объектов, которую можно перебирать (итерировать).
Основной метод:
• GetEnumerator() - возвращает объект IEnumerator.

Вот пример с кодом:

```csharp

using System;
using System.Collections;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Создаем коллекцию
        var numbers = new NumbersCollection();

        // Перебор с использованием foreach
        foreach (var number in numbers)
        {
            Console.WriteLine(number);
        }
    }
}

// Реализация коллекции, поддерживающей IEnumerable
public class NumbersCollection : IEnumerable<int>
{
    public IEnumerator<int> GetEnumerator()
    {
        // Используем yield return для генерации элементов
        for (int i = 1; i <= 5; i++)
        {
            yield return i; // возвращает элемент по одному
        }
    }

    // Реализация необобщенного интерфейса
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

```

    2.	IEnumerator

Позволяет перебирать элементы коллекции.
Основные методы и свойства:
• MoveNext() - перемещает указатель на следующий элемент коллекции.
• Current - возвращает текущий элемент.
• Reset() - сбрасывает перечислитель на начальную позицию.

    3.	ICollection

Многие коллекции реализуют этот интерфейс, такие как List, Queue, Stack, Dictionary и другие.
Представляет общую коллекцию объектов. Наследуется от IEnumerable. Основные свойства:
• Count - количество элементов.
• IsReadOnly - доступ только для чтения.
Методы:
• Add(), Remove(), Clear().

    4.	IList

Представляет коллекцию объектов, доступ к которым осуществляется по индексу. Наследуется от ICollection. Дополнительно:
• Поддерживает индексацию (this[int index]).

    5.	IDisposable

Чтобы он работал, надо обязательно использовать `using` в коде.
Указывает, что объект должен освободить ресурсы.
Метод:
• Dispose().

    6.	IComparable

Определяет метод для сравнения экземпляров.
Метод:
• CompareTo().
Если в классе реализуется IComparable, то можно использовать метод Sort() для сортировки коллекции.

    7. IEquatable

Определяет метод для сравнения экземпляров. Метод:
• Equals().

```csharp

class Person : IEquatable<Person>
{
	public string Name { get; set; }
	public int Age { get; set; }

	public bool Equals(Person other)
	{
		if (other == null)
			return false;

		return Name == other.Name && Age == other.Age;
	}
}

```

Можете конечно спросить чем отличается Equals() который находится в IEquatable от Equals() который находится в Object.
Ответ простой: Equals() в Object принимает параметр типа Object, а Equals() в IEquatable принимает параметр типа T. Вот пример такого же класса, но с переопределенным Equals() в Object:

```csharp

class Person
{
	public string Name { get; set; }
	public int Age { get; set; }

	public override bool Equals(object obj)
	{
		if (obj == null)
			return false;

		if (obj.GetType() != this.GetType())
			return false;

		Person person = (Person)obj;
		return Name == person.Name && Age == person.Age;
	}
}

```

    8.	ICloneable

Позволяет создать копию объекта. Тут такая же тема как и DeepCopy и ShallowCopy.
Вот пример:

```csharp

class Person : ICloneable
{
	public string Name { get; set; }
	public int Age { get; set; }

	public object Clone()
	{
		return new Person { Name = this.Name, Age = this.Age };
	}
}

void Main()
{
	Person person = new Person { Name = "Tom", Age = 23 };
	Person clone = (Person)person.Clone();
}

```

Такой метод копирования называется DeepCopy. Так как он создает новый объект и копирует в него все поля. Если же мы хотим создать копию объекта, но чтобы ссылочные типы остались ссылками на те же объекты, то это называется ShallowCopy. Вот пример:

```csharp

class Person
{
	public string Name { get; set; }
	public int Age { get; set; }

}

void Main()
{
	Person person = new Person { Name = "Tom", Age = 23 };
	Person clone = person;
}

```

Здесь уже не используется интерфейс ICloneable, так как мы просто присваиваем ссылку на объект.
