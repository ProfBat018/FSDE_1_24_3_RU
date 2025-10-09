# REST API в ASP.NET

## 1. Что такое REST
**REST (Representational State Transfer)** — архитектурный стиль взаимодействия клиент-серверных приложений через HTTP.  
Основная идея: ресурсы (например, пользователи, заказы, товары) представляются **URL-адресами**, а действия над ними выполняются с помощью стандартных HTTP-методов:

- `GET` – получение ресурса
- `POST` – создание ресурса
- `PUT`/`PATCH` – обновление ресурса
- `DELETE` – удаление ресурса

REST — это **принципы и ограничения**, а не конкретная технология.


![](https://www.saurabhmisra.dev/static/banner-81b66769159042b7826d74c25c30d21a.jpg)
---

## 2. Почему он нам нужен
- **Простота**: REST использует стандартные HTTP-протоколы и легко интегрируется с любыми клиентами (браузер, мобильное приложение, IoT).
- **Масштабируемость**: архитектура «клиент-сервер» облегчает разделение логики.
- **Кэширование**: HTTP-заголовки позволяют кэшировать ответы и ускорять работу.
- **Гибкость**: REST не привязан к конкретному формату данных (JSON, XML, YAML).

---

## 3. Сравнение REST и SOAP

| Характеристика | REST | SOAP |
|----------------|------|------|
| Стиль | Архитектурный стиль | Протокол |
| Формат данных | JSON, XML, любые | Только XML |
| Простота | Прост в использовании | Более сложный |
| Скорость | Легкий, быстрый | Тяжелый, медленный |
| Кэширование | Поддерживает | Практически отсутствует |
| Стандартизация | Нет строгих стандартов | Четкий стандарт WSDL |

REST чаще всего используется в веб-приложениях, в то время как SOAP — в банковской сфере и B2B-системах.

---

## 4. REST vs RESTful API
- **REST** — набор архитектурных принципов.
- **RESTful API** — это API, которое **реализует эти принципы**:
  - Использует HTTP-методы (`GET`, `POST`, `PUT`, `DELETE`).
  - Ресурсы доступны по URI.
  - Сервер не хранит состояние клиента (stateless).
  - Поддержка кэширования.

Пример:  
- **REST**: «Вот правила, как должен работать API».  
- **RESTful API**: «Наш API для пользователей реализует эти правила».

---

## 5. Пример идеального контроллера в ASP.NET

```csharp
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    // GET api/users
    [HttpGet]
    public IActionResult GetAllUsers()
    {
        var users = new List<object>
        {
            new { Id = 1, Name = "Alice" },
            new { Id = 2, Name = "Bob" }
        };
        return Ok(users);
    }

    // GET api/users/1
    [HttpGet("{id}")]
    public IActionResult GetUser(int id)
    {
        if (id == 1) return Ok(new { Id = 1, Name = "Alice" });
        return NotFound();
    }

    // POST api/users
    [HttpPost]
    public IActionResult CreateUser([FromBody] object user)
    {
        // В реальном проекте добавляем в БД
        return CreatedAtAction(nameof(GetUser), new { id = 3 }, user);
    }

    // PUT api/users/1
    [HttpPut("{id}")]
    public IActionResult UpdateUser(int id, [FromBody] object user)
    {
        if (id != 1) return NotFound();
        return NoContent();
    }

    // DELETE api/users/1
    [HttpDelete("{id}")]
    public IActionResult DeleteUser(int id)
    {
        if (id != 1) return NotFound();
        return NoContent();
    }
}
```

**Особенности:**
- Четкие методы под каждый HTTP-метод.
- Правильные коды ответов (`200 OK`, `201 Created`, `404 Not Found`, `204 No Content`).
- Route в стиле REST (`/api/users/{id}`).

---

## 6. Code by Demand (Пример)
**Code on Demand** — это опциональное ограничение REST, при котором сервер может отправлять клиенту **исполняемый код**. Например:  
- JavaScript для браузера
- WebAssembly для ускорения работы
- Логика, которую клиент выполняет локально

### Пример: чат-бот на сайте
1. Пользователь заходит на сайт.
2. Браузер отправляет `GET /api/chatbot/script`.
3. Сервер возвращает **JavaScript-код**, который:
   - отображает кнопку «Чат» на сайте,
   - подключает веб-сокет к API,
   - обрабатывает ответы.

```javascript
// Пример кода, который сервер может вернуть
(function() {
    let chatButton = document.createElement("button");
    chatButton.innerText = "Chat with us";
    chatButton.onclick = () => alert("Connecting to chatbot...");
    document.body.appendChild(chatButton);
})();
```

---

## 7. Наименования конечных точек
- Использовать существительные во множественном числе (`/api/users`).
- Не включать глаголы в маршруты (действие определяется HTTP-методом).
- Использовать вложенные маршруты для связей (`/api/users/1/orders`).
- Выбрать единый стиль (kebab-case или snake_case).

---

# Итоги
- REST — это стиль проектирования API.
- REST нужен для простоты, гибкости и масштабируемости.
- REST проще и легче, чем SOAP.
- RESTful API = REST на практике.
- Хороший контроллер должен соответствовать принципам REST.
- Code by Demand позволяет серверу «обучать» клиента новому поведению.
