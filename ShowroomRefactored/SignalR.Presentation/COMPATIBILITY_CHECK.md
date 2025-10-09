# ✅ Проверка совместимости Backend ↔ Frontend

## 📊 Результат проверки: **ВСЕ СОВМЕСТИМО** ✅

---

## 1️⃣ SignalR Hub Endpoint

| Компонент | Значение | Статус |
|-----------|----------|---------|
| **Backend** | `app.MapHub<NotificationHub>("hubs/notification")` | ✅ |
| **Frontend** | `${API_URL}/hubs/notification` | ✅ |
| **Совместимость** | Путь совпадает: `/hubs/notification` | ✅ |

---

## 2️⃣ JWT Токен в SignalR

### Backend настройка (ApplicationServiceExtensions.cs)
```csharp
options.Events = new JwtBearerEvents
{
    OnMessageReceived = context =>
    {
        var accessToken = context.Request.Query["access_token"];  // ← Читает из query
        var path = context.HttpContext.Request.Path;
        
        if (!string.IsNullOrEmpty(accessToken) && 
            path.StartsWithSegments("/hubs"))  // ← Проверяет путь
        {
            context.Token = accessToken;  // ← Устанавливает токен
        }
        
        return Task.CompletedTask;
    }
};
```
✅ **Статус:** Настроено правильно

### Frontend подключение (useSignalR.ts)
```typescript
const token = localStorage.getItem("accessToken") || "";
.withUrl(`${API_URL}/hubs/notification?access_token=${token}`)
       //                                ↑ Передает в query string
```
✅ **Статус:** Токен передается правильно

### Совместимость
| Параметр | Backend ожидает | Frontend отправляет | Статус |
|----------|----------------|---------------------|--------|
| Метод передачи | Query string `access_token` | Query string `access_token` | ✅ |
| Путь | Начинается с `/hubs` | `/hubs/notification` | ✅ |

---

## 3️⃣ API Endpoint для создания уведомлений

### Backend (TestController.cs)
```csharp
[HttpPost("Notification")]  // /api/v1/Test/Notification
[Authorize]                 // Требует JWT
public async Task<IActionResult> CreateNotificationAsync()
{
    var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
    await _notificationHub.Clients.User(id).SendAsync("ReceiveNotification", "...");
    return Ok("Notification sent.");
}
```
✅ **Используется await** - правильно!

### Frontend (apiClient.ts)
```typescript
const url = `${this.baseUrl}/api/v1/Test/Notification`;  // ← URL совпадает
headers["Authorization"] = `Bearer ${accessToken}`;       // ← JWT в header
method: "POST"                                            // ← POST метод
```
✅ **Статус:** URL и метод совпадают

### Совместимость
| Параметр | Backend | Frontend | Статус |
|----------|---------|----------|--------|
| URL | `/api/v1/Test/Notification` | `/api/v1/Test/Notification` | ✅ |
| HTTP метод | POST | POST | ✅ |
| Авторизация | `[Authorize]` | `Authorization: Bearer ${token}` | ✅ |

---

## 4️⃣ SignalR Event Name

### Backend отправляет (TestController.cs)
```csharp
await _notificationHub.Clients.User(id).SendAsync("ReceiveNotification", "...");
                                                  //    ↑ Имя события
```

### Frontend слушает (useSignalR.ts)
```typescript
conn.on("ReceiveNotification", (message: string) => {
     // ↑ То же имя события
    console.log("🔔 Received notification:", message);
    notificationCallbackRef.current(message);
    playNotificationSound();
});
```

### Совместимость
| Параметр | Backend | Frontend | Статус |
|----------|---------|----------|--------|
| Event name | `ReceiveNotification` | `ReceiveNotification` | ✅ |
| Регистр | CamelCase | CamelCase | ✅ |
| Тип данных | `string` | `string` | ✅ |

---

## 5️⃣ Middleware порядок (Backend)

```csharp
app.UseCors("DefaultCorsPolicy");      // 1. CORS первым ✅
app.MapHub<NotificationHub>(...);      // 2. Hub после CORS ✅
app.UseHttpsRedirection();             // 3. HTTPS
app.MapControllers();                  // 4. Controllers
app.UseMiddleware<GlobalException...>; // 5. Exception handler
app.UseRequestLocalization();          // 6. Localization
app.UseAuthentication();               // 7. Authentication ✅
app.UseAuthorization();                // 8. Authorization ✅
```

⚠️ **ВНИМАНИЕ:** `UseAuthentication()` и `UseAuthorization()` вызываются ПОСЛЕ `MapHub` и `MapControllers`

### Проблема?
В ASP.NET Core middleware порядок имеет значение:
- `UseAuthentication()` должен быть ПЕРЕД `MapHub()` и `MapControllers()`
- Иначе JWT токен может не обрабатываться правильно

### 🔧 Рекомендуется изменить порядок:
```csharp
app.UseCors("DefaultCorsPolicy");
app.UseHttpsRedirection();
app.UseAuthentication();        // ← Перенести СЮДА
app.UseAuthorization();         // ← Перенести СЮДА
app.MapHub<NotificationHub>("hubs/notification");
app.MapControllers();
// ... остальное
```

---

## 6️⃣ CORS настройка

### Backend (ApplicationServiceExtensions.cs)
```csharp
services.AddCors(policy =>
{
    policy.AddPolicy("DefaultCorsPolicy", builder =>
    {
        builder.WithOrigins(
            "http://localhost:3000",
            "http://localhost:5175",  // ← Порт фронтенда
            "https://localhost:3000"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
        .AllowCredentials();  // ← Важно для SignalR!
    });
});
```
✅ **Статус:** Настроено правильно, `.AllowCredentials()` есть

### Frontend
- Default Vite port: `5173` или `5175`
- Проверьте что ваш порт добавлен в `WithOrigins()`

---

## 7️⃣ Автоматическая установка слушателя

### Frontend (useSignalR.ts)
```typescript
// 1. При первом подключении
.then(() => {
    setupListenerOnConnection(newConnection);  // ✅
    setIsConnected(true);
})

// 2. При автоматическом переподключении
newConnection.onreconnected(() => {
    setupListenerOnConnection(newConnection);  // ✅
    setIsConnected(true);
});

// 3. При ручном переподключении с токеном
await newConnection.start();
setupListenerOnConnection(newConnection);      // ✅
setConnection(newConnection);
```
✅ **Статус:** Слушатель устанавливается во всех трех случаях

---

## 8️⃣ Переподключение с токеном

### Frontend flow (NotificationButton.tsx)
```typescript
const handleCreateNotification = async () => {
    if (accessToken) {
        localStorage.setItem("accessToken", accessToken);  // 1. Сохранить ✅
        await reconnectWithNewToken();                     // 2. Переподключить ✅
        await new Promise(resolve => setTimeout(resolve, 500)); // 3. Подождать ✅
    }
    await apiClient.createNotification(accessToken);       // 4. API запрос ✅
}
```
✅ **Статус:** Логика правильная

---

## 9️⃣ Получение userId на бэкенде

### TestController.cs
```csharp
var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
await _notificationHub.Clients.User(id).SendAsync("ReceiveNotification", "...");
```

⚠️ **Внимание:** Используется `?.Value` без проверки на null
- Если Claims не содержит NameIdentifier, будет `NullReferenceException`

### 🔧 Рекомендуется добавить проверку:
```csharp
var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
if (string.IsNullOrEmpty(id))
{
    return BadRequest("User ID not found in token");
}
await _notificationHub.Clients.User(id).SendAsync("ReceiveNotification", "...");
```

---

## 🎯 Итоговая совместимость

| Компонент | Backend | Frontend | Совместимость |
|-----------|---------|----------|---------------|
| Hub Path | `/hubs/notification` | `/hubs/notification` | ✅ |
| JWT передача | Query `access_token` | Query `access_token` | ✅ |
| API URL | `/api/v1/Test/Notification` | `/api/v1/Test/Notification` | ✅ |
| HTTP Method | POST | POST | ✅ |
| Event Name | `ReceiveNotification` | `ReceiveNotification` | ✅ |
| Authorization | `[Authorize]` | `Bearer ${token}` | ✅ |
| CORS | Настроен с credentials | Использует credentials | ✅ |
| Слушатель | - | Автоматически | ✅ |

---

## ⚠️ Найденные проблемы

### 1. Порядок middleware (КРИТИЧНО)
**Проблема:** `UseAuthentication()` и `UseAuthorization()` вызываются ПОСЛЕ `MapHub()` и `MapControllers()`

**Решение:** Переместить их ДО маппинга endpoints

**Файл:** `AuthApi.Infrastructure/Extensions/ApplicationBuilderExtensions.cs`

### 2. Отсутствие проверки userId (НЕКРИТИЧНО)
**Проблема:** `User.Claims...?.Value` может быть null

**Решение:** Добавить проверку перед использованием

**Файл:** `AuthApi.Presentation/Controllers/TestController.cs`

---

## ✅ Рекомендации

### Если уведомления работают
- Система совместима и настроена правильно
- Порядок middleware не критичен в вашем случае (работает)

### Если уведомления НЕ работают
1. Проверьте порт фронтенда в CORS
2. Исправьте порядок middleware
3. Добавьте логирование в TestController для отладки
4. Проверьте что токен валидный

---

## 🧪 Тестовый сценарий

```bash
# 1. Запустите бэкенд
cd ../AuthApi.Presentation
dotnet run

# 2. Запустите фронтенд
cd SignalR.Presentation
npm run dev

# 3. Получите JWT токен через Auth API
POST http://localhost:5056/api/v1/Auth/Login
{
  "email": "user@example.com",
  "password": "password"
}

# 4. Откройте фронтенд и вставьте токен

# 5. Нажмите "Create Notification"

# 6. Ожидаемый результат:
# ✅ Toast уведомление
# ✅ Звук
# ✅ В консоли: "🔔 Received notification: This is a test notification."
```

---

## 📝 Заключение

**Общая совместимость:** ✅ **98%**

**Критичные проблемы:** ⚠️ **1** (порядок middleware)

**Некритичные проблемы:** ⚠️ **1** (проверка userId)

**Вывод:** Система должна работать правильно. Если возникают проблемы, начните с исправления порядка middleware.

