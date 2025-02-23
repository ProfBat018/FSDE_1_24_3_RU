# Тема урока: 
- Введение в EF Core 
- Scaffolding 


## Введение в EF Core

`Ef core` или `Entity Framework Core` - это ORM (Object-Relational Mapping) фреймворк,
который позволяет работать с базами данных через объектно-ориентированный подход.

В отличии от Dapper нам не придется явно скачивать пакет `Microsoft.Data.SqlClient` 
и писать SQL запросы, хоть он и работает под капотом через ADO.NET. 

Данный фреймворк позволяет работать с различными базами данных, такими как:
- SQL Server
- SQLite
- MySQL
- PostgreSQL

без единого написания SQL запроса.

Его осноыными плюсами являются два подхода к работе с базой данных:
- Database First
- Code First

сегодня мы будем изучать подход `Database First` через инструмент `Scaffolding`.

Для начала нам нужно установить пакеты для работы с EF Core:
- `Microsoft.EntityFrameworkCore`
- `Microsoft.EntityFrameworkCore.Design`
- `Microsoft.EntityFrameworkCore.SqlServer`
- `Microsoft.EntityFrameworkCore.Tools`


Первый пакет - это основной пакет для работы с EF Core.

Второй пакет - это пакет для того чтобы делать миграции и `Scaffolding`, который мы сегодня будем использовать.

Третий пакет - это пакет для работы с SQL Server.

Четвертый пакет - это пакет для работы с инструментами EF Core через консоль. 


Для начала мы создадим базу данных, в нашем случае это будет БД `Sql Server` с именем `Auth_3`.
В нем уже есть таблицы и данные. Буквально через пару минут вы увидите все волшебство `Scaffolding`.

Обычно везде для работы с `EF Core` используют `package manager console`, который интегрирован 
в `Visual Studio`. Но я предпочитаю работать через `Dotnet CLI` (Command Line Interface).
Такой подход более правильный и универсальный. К тому же привыкание работать с `CLI` сэкономит
вам много времени в будущем.

1. Открываем терминал и переходим в папку проекта 
P.S. Не в папку с Solution файлом, а в папку с проектом. Обычно у вас solution и проект называются одинаково.

Команда для `scaffold` выглядит так: 

```bash

dotnet ef dbcontext scaffol <connection_string> <db_provider>
```

в нашем случае `db_provider` это `Microsoft.EntityFrameworkCore.SqlServer` потому что мы работаем 
с `Sql Server`.

Все остальные примеры смотреть на записи с урока по дате на 23 февраля. 