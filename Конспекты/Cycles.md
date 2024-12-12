# Тема урока: Циклы 

В общем тут нечего писать, просто примеры кода и все.

## Цикл while

```csharp
int i = 0;
while (i < 10)
{
    Console.WriteLine(i);
    i++;
}
```

## Цикл do while

```csharp

int i = 0;

do
{
    Console.WriteLine(i);
    i++;
} while (i < 10);
```

## Цикл for

```csharp
for (int i = 0; i < 10; i++)
{
    Console.WriteLine(i);
}
```

Есть еще и нововведение с for, начиная с C# 5.0, можно использовать цикл foreach для перебора элементов коллекции.

```csharp
int[] numbers = { 4, 5, 6, 7, 8, 9, 10 };

foreach (int i in numbers)
{
    Console.WriteLine(i);
}
```

## Операторы break и continue

```csharp

for (int i = 0; i < 10; i++)
{
    if (i == 5)
    {
        break;
    }
    Console.WriteLine(i);
}

for (int i = 0; i < 10; i++)
{
    if (i == 5)
    {
        continue;
    }
    Console.WriteLine(i);
}
```

Не забывайте про то что в `foreach` нельзя менять коллекцию, по которой происходит перебор. Если вам нужно это сделать, то используйте обычный цикл `for`.

```csharp

List<int> numbers = new() { 4, 5, 6, 7, 8, 9, 10 };

foreach (int i in numbers)
{
    i = 5;
}

```



