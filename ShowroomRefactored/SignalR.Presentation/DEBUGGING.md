# 🐛 Отладка SignalR уведомлений

## Что изменилось в коде

### Ключевое изменение
**Слушатель теперь устанавливается автоматически** при каждом подключении/переподключении:

1. ✅ При первом подключении (в `useEffect`)
2. ✅ При автоматическом переподключении (`onreconnected`)
3. ✅ При ручном переподключении с новым токеном (`reconnectWithNewToken`)

### Новая архитектура
```typescript
// Callback хранится в ref
notificationCallbackRef.current = вашCallbackИзКомпонента

// Слушатель устанавливается на соединение
connection.on("ReceiveNotification", (message) => {
  // Вызывается ваш callback из компонента
  notificationCallbackRef.current(message)
})
```

## 📋 Чеклист проверки

### 1. Проверьте CORS на бэкенде
Убедитесь, что в `ApplicationServiceExtensions.cs` есть:

```csharp
services.AddCors(policy =>
{
    policy.AddPolicy("DefaultCorsPolicy", builder =>
    {
        builder.WithOrigins(
            "http://localhost:3000", 
            "http://localhost:5175", // <- ваш порт фронтенда
            "https://localhost:3000"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials(); // <- ВАЖНО для SignalR
    });
});
```

### 2. Проверьте порядок middleware в `ApplicationBuilderExtensions.cs`

```csharp
app.UseCors("DefaultCorsPolicy"); // <- ПЕРВЫМ
app.MapHub<NotificationHub>("hubs/notification"); // <- Hub ПОСЛЕ CORS
app.UseHttpsRedirection();
app.MapControllers();
// ... остальное
app.UseAuthentication();
app.UseAuthorization();
```

### 3. Проверьте JWT аутентификацию

В `ApplicationServiceExtensions.cs` должна быть настройка для SignalR:

```csharp
.AddJwtBearer(options =>
{
    // ... обычные настройки JWT ...

    // Настройка для SignalR
    options.Events = new JwtBearerEvents
    {
        OnMessageReceived = context =>
        {
            var accessToken = context.Request.Query["access_token"];
            var path = context.HttpContext.Request.Path;
            
            if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
            {
                context.Token = accessToken;
            }
            
            return Task.CompletedTask;
        }
    };
});
```

## 🧪 Пошаговая проверка

### Шаг 1: Запустите бэкенд
```bash
cd ../AuthApi.Presentation
dotnet run
```

### Шаг 2: Запустите фронтенд
```bash
npm run dev
```

### Шаг 3: Откройте DevTools Console (F12)

Вы должны увидеть:
```
Setting up SignalR connection to: http://localhost:5056/hubs/notification
✅ SignalR Connected successfully
✅ ReceiveNotification listener установлен на соединение
📝 Registering notification callback
```

### Шаг 4: Получите JWT токен

Используйте Postman или любой другой HTTP клиент:

**POST** `http://localhost:5056/api/v1/Auth/Login`
```json
{
  "email": "your@email.com",
  "password": "yourpassword"
}
```

Скопируйте `accessToken` из ответа.

### Шаг 5: Вставьте токен на фронтенде

1. Вставьте токен в поле "Access Token"
2. Нажмите "Create Notification"

В консоли должно появиться:
```
🔑 Token saved to localStorage for SignalR
🔄 Reconnecting SignalR with new token...
✅ SignalR Reconnected with new token
✅ ReceiveNotification listener установлен на соединение
🚀 Sending notification request to: http://localhost:5056/api/v1/Test/Notification
✅ Notification created successfully
🔔 Received notification: This is a test notification.
```

## ❌ Типичные проблемы и решения

### Проблема 1: "Access to XMLHttpRequest has been blocked by CORS policy"

**Причина:** CORS не настроен или порт не добавлен

**Решение:**
1. Добавьте ваш порт фронтенда в `WithOrigins()`
2. Убедитесь что `AllowCredentials()` присутствует
3. `UseCors()` должен быть ПЕРЕД `MapHub()`

### Проблема 2: "401 Unauthorized" при подключении к SignalR

**Причина:** JWT токен не передается или неверный

**Решение:**
1. Проверьте что токен сохранен в localStorage: `localStorage.getItem("accessToken")`
2. Проверьте что настроен `OnMessageReceived` в JWT конфигурации
3. Убедитесь что токен валидный (не истек)

### Проблема 3: Подключение есть, но уведомления не приходят

**Причина:** Слушатель не установлен или метод на бэкенде неправильный

**Решение:**
1. В консоли должно быть: `✅ ReceiveNotification listener установлен`
2. Проверьте что на бэкенде используется: `Clients.User(id).SendAsync("ReceiveNotification", ...)`
3. Проверьте что userId извлекается из JWT правильно:
   ```csharp
   var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
   ```

### Проблема 4: "Cannot read property 'current' of undefined"

**Причина:** Callback не зарегистрирован

**Решение:**
Убедитесь что в компоненте вызывается:
```typescript
useEffect(() => {
  if (isConnected) {
    setupNotificationListener((message: string) => {
      // ваш код
    });
  }
}, [isConnected, setupNotificationListener]);
```

## 🔍 Дополнительная отладка

### Проверка на бэкенде

Добавьте логирование в `TestController`:

```csharp
[HttpPost("Notification")]
public async Task<IActionResult> CreateNotificationAsync()
{
    var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    
    Console.WriteLine($"🔍 User ID from claims: {id}");
    Console.WriteLine($"🔍 Sending notification to user: {id}");
    
    await _notificationHub.Clients.User(id).SendAsync("ReceiveNotification", "This is a test notification.");
    
    Console.WriteLine($"✅ Notification sent");
    
    return Ok("Notification sent.");
}
```

### Проверка ConnectionId

Добавьте в Hub метод для проверки:

```csharp
public class NotificationHub : Hub
{
    public override async Task OnConnectedAsync()
    {
        var userId = Context.UserIdentifier;
        Console.WriteLine($"🔗 User {userId} connected with ConnectionId: {Context.ConnectionId}");
        await base.OnConnectedAsync();
    }
}
```

### Проверка на фронтенде

Нажмите кнопку "🔍 Debug" и проверьте вывод:
- `isConnected` должен быть `true`
- `connection` не должен быть `null`
- `connection state` должен быть `"Connected"`
- `connection ID` должен быть строкой (не null)

## 📞 Если ничего не помогло

1. Перезапустите оба сервера (бэкенд и фронтенд)
2. Очистите localStorage: `localStorage.clear()`
3. Очистите кэш браузера (Ctrl+Shift+Del)
4. Попробуйте в режиме инкогнито
5. Проверьте что оба сервера запущены на правильных портах:
   - Backend: `http://localhost:5056`
   - Frontend: `http://localhost:5175` (или проверьте в консоли при запуске Vite)

## ✅ Критерии успеха

Все работает правильно если:
- ✅ Зеленый индикатор "Connected"
- ✅ В консоли: `✅ ReceiveNotification listener установлен`
- ✅ При нажатии "Create Notification" появляется toast уведомление
- ✅ В консоли: `🔔 Received notification: This is a test notification.`
- ✅ Проигрывается звук уведомления

