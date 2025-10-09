# ✅ Финальная настройка SignalR с JWT токеном

## 🎯 Как работает система

### Архитектура
```
┌─────────────────────────────────────────────────────┐
│                    FRONTEND                         │
│                                                     │
│  1. Подключается к SignalR с токеном                │
│     ws://localhost:5056/hubs/notification?access_token=JWT
│                                                     │
│  2. Отправляет POST запрос с токеном                │
│     POST /api/v1/Test/Notification                  │
│     Authorization: Bearer JWT                       │
└─────────────────────────────────────────────────────┘
                       │
                       ▼
┌─────────────────────────────────────────────────────┐
│                    BACKEND                          │
│                                                     │
│  1. SignalR Hub получает токен и определяет userId  │
│     Context.UserIdentifier = userId из JWT          │
│                                                     │
│  2. TestController получает запрос                  │
│     - Извлекает userId из JWT Claims                │
│     - Отправляет: Clients.User(userId).SendAsync()  │
│                                                     │
│  3. SignalR находит подключение по userId           │
│     и отправляет уведомление                        │
└─────────────────────────────────────────────────────┘
```

## 📝 Пошаговая инструкция

### Шаг 1: Получите JWT токен

Используйте любой HTTP клиент для входа:

**POST** `http://localhost:5056/api/v1/Auth/Login`
```json
{
  "email": "your@email.com",
  "password": "yourpassword"
}
```

Скопируйте `accessToken` из ответа.

### Шаг 2: Запустите фронтенд

```bash
npm run dev
```

### Шаг 3: Вставьте токен

1. Откройте `http://localhost:5175`
2. Вставьте JWT токен в поле "Access Token"
3. Нажмите "Create Notification"

### Что происходит при нажатии кнопки:

1. ✅ Токен сохраняется в `localStorage`
2. ✅ SignalR переподключается с новым токеном
3. ✅ Отправляется POST запрос на API
4. ✅ Бэкенд отправляет уведомление через SignalR
5. ✅ Фронтенд получает уведомление и показывает toast + звук

## 🔍 Как проверить что все работает

### В консоли браузера должно быть:

```
Setting up SignalR connection to: http://localhost:5056/hubs/notification without token
✅ SignalR Connected successfully
✅ ReceiveNotification listener установлен на соединение
📝 Registering notification callback

[После вставки токена и нажатия кнопки:]
🔑 Token saved to localStorage
🔄 Reconnecting SignalR with new token...
✅ SignalR Reconnected with new token
✅ ReceiveNotification listener установлен на соединение
🚀 Sending notification request to: http://localhost:5056/api/v1/Test/Notification
✅ Notification created successfully
🔔 Received notification: This is a test notification.
```

## ⚙️ Как это работает технически

### 1. Первое подключение (без токена)
```typescript
// useSignalR.ts
const createConnection = () => {
  const token = localStorage.getItem("accessToken") || "";
  // Сначала токена нет, подключается без него
  return new signalR.HubConnectionBuilder()
    .withUrl(`${API_URL}/hubs/notification?access_token=${token}`)
    .build();
}
```

### 2. Переподключение с токеном
```typescript
// NotificationButton.tsx
const handleCreateNotification = async () => {
  if (accessToken) {
    localStorage.setItem("accessToken", accessToken); // Сохраняем
    await reconnectWithNewToken(); // Переподключаемся
  }
  await apiClient.createNotification(accessToken); // API запрос
}
```

### 3. Бэкенд получает токен

**В ApplicationServiceExtensions.cs:**
```csharp
options.Events = new JwtBearerEvents
{
    OnMessageReceived = context =>
    {
        var accessToken = context.Request.Query["access_token"];
        var path = context.HttpContext.Request.Path;
        
        if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/hubs"))
        {
            context.Token = accessToken; // ← SignalR получает токен
        }
        
        return Task.CompletedTask;
    }
};
```

### 4. SignalR определяет userId

После валидации JWT, SignalR автоматически устанавливает `Context.UserIdentifier` на основе ClaimTypes.NameIdentifier из токена.

### 5. Отправка уведомления конкретному пользователю

**TestController.cs:**
```csharp
var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
await _notificationHub.Clients.User(id).SendAsync("ReceiveNotification", "...");
```

`Clients.User(id)` находит SignalR подключение по userId и отправляет уведомление!

## 🐛 Типичные проблемы

### Проблема 1: Уведомления не приходят

**Причина:** SignalR подключен без токена, поэтому `Clients.User(id)` не находит пользователя.

**Решение:** 
1. Вставьте JWT токен в поле
2. Нажмите "Create Notification" - произойдет переподключение с токеном

### Проблема 2: "401 Unauthorized"

**Причина:** Токен невалидный или истек.

**Решение:** Получите новый токен через `/api/v1/Auth/Login`

### Проблема 3: CORS ошибка

**Причина:** Порт фронтенда не добавлен в CORS политику.

**Решение:** Проверьте `ApplicationServiceExtensions.cs`:
```csharp
builder.WithOrigins(
    "http://localhost:3000", 
    "http://localhost:5175", // ← ваш порт
    "https://localhost:3000"
)
```

## ✅ Критерии успеха

Все работает правильно если:

1. ✅ После вставки токена и нажатия кнопки происходит переподключение
2. ✅ В консоли: `🔔 Received notification: This is a test notification.`
3. ✅ Появляется toast уведомление
4. ✅ Проигрывается звук
5. ✅ Зеленый индикатор "Connected"

## 🎓 Важные моменты

### Почему нужен токен в SignalR?

`Clients.User(userId)` работает ТОЛЬКО если SignalR знает userId пользователя.
SignalR узнает userId из JWT токена при подключении.

### Почему токен передается через query string?

WebSocket (на котором работает SignalR) не поддерживает кастомные заголовки при установке соединения. Поэтому токен передается как query параметр.

### Безопасность

- JWT токен короткоживущий (обычно 15-60 минут)
- Используйте HTTPS в production
- Токен в localStorage - для простоты, в production используйте httpOnly cookies для refresh токенов

## 📦 Файлы для справки

- `src/hooks/useSignalR.ts` - логика SignalR подключения
- `src/components/NotificationButton.tsx` - UI компонент
- `src/httpclient/apiClient.ts` - API клиент
- `DEBUGGING.md` - подробное руководство по отладке
- `INTEGRATION_GUIDE.md` - полное руководство по интеграции

