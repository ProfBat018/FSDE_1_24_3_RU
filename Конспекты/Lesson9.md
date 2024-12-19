# Lesson 9:

- exceptions
- try-catch-finally
- interfaces

## Exceptions

Эту тему вы уже затрагивали во время обучения С++, но на самом деле она намного важнее чем вам может показаться.

Проброс исключений - это неотъемлемая часть программирования которая помогает вам разобраться в том что происходит в вашем коде. Возможно, вы еще не писали и не встречали проекты большого размаха, но уверяю вас, что через 4-5 месяцев вы будете работать над 3 проектами одновременно и вам будет сложно понять что происходит. В первую очередь, какое бы IDE вы не использовали вам надо перевести его на `английский` язык. Это поможет вам понимать ошибки которые вы получаете. Во вторую очередь, вам надо разобраться в механизме исключений. В третью очередь, вам надо понять как правильно читать ошибки и использовать исключения в своем коде.

### Rule number one: GET YOUR EXCEPTIONS DONE

![](./5217772.jpg)

У меня тоже есть свой свод правил как у Каламбуса из `Добро пожаловать в Zомбилэнд`.

В `C#` есть базовый класс `Exception` от которого все наследуются. В отлчии от С++, вы не можете делать `throw` все что угодно. C# - это строготипизированный язык и вы можете делать `throw` только объекты которые наследуются от `Exception`.

Как я уже говорил на предыдущих уроках, тут также есть `ApplicationException` который наследуется от `Exception`. Его нужно использовать если вы создаете свой собственный Exception. В любом случае он тоже наследуется от `Exception`.

Ключевые слова для дальнейшего изучения:

- `try`
- `catch`
- `finally`
- `StackTrace`
- `InnerException`
- `Message`

Для того чтобы обработать ошибку нужно вставить блок кода в `try`. К примеру если вы будете делить на 0, то программа упадет. Чтобы этого избежать, вам надо обернуть код в `try` и обработать ошибку в `catch`.

```csharp

try
{
    int x = 0;
    int y = 10 / x;
}
catch (Exception ex)
{
    Console.WriteLine(ex.Message);
}

```

В случае если вы не будете обрабатывать ошибку, программа перестнет работать. Это может закончится очень фатально. Ведь сейчас мы пишем всего лишь консольное приложение, а потом уже будем писать веб-приложение где ваша программа в одиночку будет обрабатывать запросы от тысяч пользователей. Просто представьте что вы написали онлайн-магазин и вдруг ваша программа упала потому что один гений купил отрицательное количество товара.

```csharp
Console.WriteLine("Start of Main");

void foo()
{
        int x = 0;
        int y = 10 / x;
}

try
{
    foo();
}
catch (Exception e)
{
    Console.WriteLine(e.Message);
}

Console.WriteLine("End of Main");
```

### StackTrace

Военные данным термином называют след от пули в темноте. Возможно кто-то из вас слышал про трассирующие пули. Так вот, `StackTrace` - это свойство класса `Exception` которое показывает вам в каких местах была ошибка снизу вверх.

Давайте предположим что вы скачали библиотеку и используете какой-то ее метод в классе который создали, назовем его `TestClass` и библиотека `TestLibMethod`. Вызовем `TestLibMethod`, который внутри `TestClass` который отсылается на `TestLibMethod`. Если внутри `TestLibMethod` произойдет ошибка, то `StackTrace` покажет вам в каком месте была ошибка. Если взять реальный пример, то мы можем скачать какие-то данные из интернета, но я покажу вам на реалоном проекте из утренней группы.

```bash

 ---> System.InvalidOperationException: Unable to resolve service for type 'AutoMapper.Mapper' while attempting to activate 'Movies.Services.Classes.MovieService'.
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteFactory.CreateArgumentCallSites(ServiceIdentifier serviceIdentifier, Type implementationType, CallSiteChain callSiteChain, ParameterInfo[] parameters, Boolean throwIfCallSiteNotFound)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteFactory.CreateConstructorCallSite(ResultCache lifetime, ServiceIdentifier serviceIdentifier, Type implementationType, CallSiteChain callSiteChain)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteFactory.TryCreateExact(ServiceDescriptor descriptor, ServiceIdentifier serviceIdentifier, CallSiteChain callSiteChain, Int32 slot)
   at Microsoft.Extensions.DependencyInjection.ServiceLookup.CallSiteFactory.GetCallSite(ServiceDescriptor serviceDescriptor, CallSiteChain callSiteChain)
   at Microsoft.Extensions.DependencyInjection.ServiceProvider.ValidateService(ServiceDescriptor descriptor)
   --- End of inner exception stack trace ---
   at Microsoft.Extensions.DependencyInjection.ServiceProvider.ValidateService(ServiceDescriptor descriptor)
   at Microsoft.Extensions.DependencyInjection.ServiceProvider..ctor(ICollection`1 serviceDescriptors, ServiceProviderOptions options)
   --- End of inner exception stack trace ---
   at Microsoft.Extensions.DependencyInjection.ServiceProvider..ctor(ICollection`1 serviceDescriptors, ServiceProviderOptions options)
   at Microsoft.Extensions.DependencyInjection.ServiceCollectionContainerBuilderExtensions.BuildServiceProvider(IServiceCollection services, ServiceProviderOptions options)
   at Microsoft.Extensions.Hosting.HostApplicationBuilder.Build()
   at Microsoft.AspNetCore.Builder.WebApplicationBuilder.Build()
   at Program.<Main>$(String[] args) in /Users/wayne/Documents/Work/FBMS_3_22_8_ru/Microservices/Part1/Movies/Program.cs:line 71


```

### InnerException

`InnerException` - это свойство класса `Exception` которое показывает вам вложенные ошибки. В примере выше, вы видите что у нас есть `InnerException` который показывает вам вложенные ошибки.

### Message

`Message` - это свойство класса `Exception` которое показывает вам сообщение об ошибке.

### Finally

Данное ключевое слово для вас новое. Но на самом деле в С# есть много моментов где он используется под капотом. Он используется для того чтобы закрыть ресурсы или подключения. Предположим что вы открыли файл или подключились к серверу для записи данных. Произойдет ошибка или нет вам все равно нужно закрыть связь с сервером или файлом. Для этого и используется `finally`. Данное ключевое слово можно использовать только вместе с `try` и `catch`. Также, его можно использовать без `catch`. В будущем мы будем проходить предмет `Системное программирование` и там мы пройдем ключевое слово `lock` которое под капотом компилируется в `try-finally`.

Далее в коде я вам покажу как надо создавать свои исколючения и как надо их правильно использовать. Но это будет в следующий раз когда я сам с нулся буду писать проект для вас. А пока давайте разберемся в том, что такое интерфейсы. Данная тема заслуживает отдельного проекта, так что продолжение в Lesson10.
