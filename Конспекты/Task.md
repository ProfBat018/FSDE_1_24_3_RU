# Тема урока: Task 
- Что такое `Task` ?
- Как работает `Task` ?
- Как использовать `Task` ?
- Какие методы есть у `Task` ?

## TPL (Task Parallel Library)

В целом у нас идет такая иерархия (async/await) => Task => ThreadPool => Thread 

Мы с вами еще не прошли ключевые слова async/await, но уже прошли ThreadPool и Thread. 
Как мы уже поняли, Task под капотом использует ThreadPool, но при этом он предоставляет нам более высокоуровневый API для работы с потоками. То есть мы можем упростить себе жизнь для работы с потоками используя `Task`, зная что он использует `ThreadPool` под капотом. 

- Что такое `Task` ?

`Task` - это класс, который представляет собой асинхронную операцию. Он позволяет нам выполнять операции в фоновом режиме и получать результат, когда он будет готов. `Task` также предоставляет методы для обработки ошибок и отмены операций. 

С помощью него мы можем выполнять операции асинхронно, не блокируя основной поток. Для синхронизации между потоками мы можем использовать `Task.Wait`, `Task.Result` и другие методы. 

## Как использовать Task?

Чтобы создать и запустить задачу, можно использовать `Task.Run`, `Task.Factory.StartNew`, или просто вызвать асинхронный метод, возвращающий `Task`. Примеры:

```csharp
Task.Run(() => {
    // Некоторая работа в другом потоке
    Console.WriteLine("Выполняется в фоновом потоке");
});

Task<int> task = Task.Run(() => {
    return 42;
});

int result = task.Result; // Блокирует поток до получения результата
```

Также мы можем использовать `async` и `await`, чтобы сделать код более читаемым (рассмотрим позже).

## Какие методы есть у Task?

Некоторые основные методы и свойства класса `Task`:

- `Start()`: запускает задачу (если она была создана с помощью конструктора)
- `Wait()`: блокирует текущий поток до завершения задачи
- `Result`: возвращает результат задачи (если `Task<T>`)
- `IsCompleted`: указывает, завершена ли задача
- `IsFaulted`: указывает, завершилась ли задача с ошибкой
- `IsCanceled`: указывает, была ли задача отменена
- `ContinueWith()`: позволяет выполнять другую задачу после завершения текущей

Пример с ContinueWith:

```csharp
Task.Run(() => {
    return 10;
}).ContinueWith(task => {
    Console.WriteLine($"Результат: {task.Result}");
});
```

В других языках программирования, например в C++, аналогом `Task` является `std::future`, который также позволяет выполнять асинхронные операции и получать результаты или же в JavaScrupt это `Promise`. 

### Пример 

```csharp

Console.WriteLine("Start of main");

Task.Run(() =>
{
    Console.WriteLine($"Task 1, IsThreadPool: {Thread.CurrentThread.IsThreadPoolThread}");
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"{i} from Task");
    }
});


var th1 = new Thread(() =>
{
    Console.WriteLine($"Thread 1, IsThreadPool: {Thread.CurrentThread.IsThreadPoolThread}");
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"{i} from Thread");
    }
});
th1.Start();

th1.Join();

Console.WriteLine("End of main");

```

В примере выше мы создаем задачу с помощью `Task.Run`, которая выполняется в пуле потоков, и поток с помощью `new Thread`. В разных запусках мы видим разные результаты, но можно увдитеть что 
их вывод может чередоваться, что значит что они могут выполняться параллельно. 

Но если мы запустим наш Task с методом `Wait`, то мы увидим что он будет блокировать основной поток до тех пор пока не завершится задача. 

```csharp 

Console.WriteLine("Start of main");

Task.Run(() =>
{
    Console.WriteLine($"Task 1, IsThreadPool: {Thread.CurrentThread.IsThreadPoolThread}");
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"{i} from Task");
    }
}).Wait();

var th1 = new Thread(() =>
{
    Console.WriteLine($"Thread 1, IsThreadPool: {Thread.CurrentThread.IsThreadPoolThread}");
    for (int i = 0; i < 5; i++)
    {
        Console.WriteLine($"{i} from Thread");
    }
});
th1.Start();

th1.Join();

Console.WriteLine("End of main");
```

## Разница между Task с Wait() и без него

При использовании `Task.Run(...)` задача запускается в пуле потоков и выполняется асинхронно. Но поведение программы зависит от того, вызываем ли мы `.Wait()` или нет.

### Без `.Wait()`

```csharp
Task.Run(() => {
    Console.WriteLine("Task выполняется");
});

Console.WriteLine("End of main");
```

В этом случае `Task` запускается, но основной поток не ждет её завершения. Поэтому `End of main` может быть выведено раньше, чем сообщение из задачи.

**Возможный вывод:**
```
End of main
Task выполняется
```

### С `.Wait()`

```csharp
Task.Run(() => {
    Console.WriteLine("Task выполняется");
}).Wait();

Console.WriteLine("End of main");
```

Здесь `.Wait()` блокирует основной поток до завершения задачи. Поэтому порядок вывода всегда будет предсказуемым: сначала задача, потом окончание.

**Вывод:**
```
Task выполняется
End of main
```

### Когда использовать `.Wait()`?

Используйте `.Wait()`, если необходимо дождаться завершения задачи **перед выполнением следующего кода**. В противном случае, если задача может выполняться параллельно, можно не вызывать `Wait()`.
