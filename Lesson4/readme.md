# Тема урока: DML

- DML
- Вставка данных
- Обновление данных
- Выборка данных
- - ALIAS
- Удаление данных
- - IN, BETWEEN, LIKE
- - TOP, CASE WHEN
- - IIF
- Foreign Key
- Нормализация таблиц
- JOIN и UNION, UNION ALL
- Агрегатные функции
- - Декартово произведение
- P.S. Truncate из DDL
- P.S. Alter из DDL
- Создание таблиц для Академии


## DML

`DML` (Data Manipulation Language) - язык манипуляции данными. Он используется для вставки, обновления, удаления и выборки данных из таблицы.

В синтаксис `DML` входят следующие команды:

- `SELECT` - выборка данных
- `INSERT` - вставка данных
- `UPDATE` - обновление данных
- `DELETE` - удаление данных

и есть добавочные команды:

- `WHERE` - условие
- `ORDER BY` - сортировка
- `GROUP BY` - группировка
- `HAVING` - условие для группировки
- `JOIN` - объединение таблиц
- `UNION` - объединение результатов запросов


## Вставка данных

`INSERT INTO` - команда для вставки данных в таблицу.

Есть два способа вставки данных:

1. Вставка данных во все столбцы таблицы:
```sql
INSERT INTO Students VALUES (N'Elvin', N'Azimov', N'elvin.azim@outlook.com', '16.11.2001');
```

2. Вставка данных в определенные столбцы таблицы:
```sql
INSERT INTO Students (Name, Surname, Email, BirthDate)
VALUES (N'Elvin', N'Azimov', N'elvin.azim@outlook.com', '16.11.2001');
```

Я считаю что второй способ более предпочтительный,
так как он позволяет вставлять данные в определенные столбцы таблицы, 
что упрощает работу с данными.

## Обновление данных

`UPDATE` - команда для обновления данных в таблице.

```sql

UPDATE Academy.[dbo].Students
SET Name = N'Samir'
WHERE Id = 1;
```

Можно и без [DBO]. 

Обновление не происходит без условия `WHERE`,
так как это приведет к обновлению всех строк в таблице.

## Выборка данных

`SELECT` - команда для выборки данных из таблицы.

```sql

SELECT * FROM Students;
```

Можно выбирать данные из определенных столбцов:

```sql

SELECT Name, Surname FROM Students;
```

Можно использовать `ALIAS` для столбцов:

```sql

SELECT Name AS 'First Name', Surname AS 'Last Name' FROM Students;
```

## Удаление данных

`DELETE` - команда для удаления данных из таблицы.

```sql

DELETE FROM Students WHERE Id = 1;
```

Данный запрос удалит строку с `Id = 1`. Но вы можете удалить все сразу:

```sql

DELETE FROM Students;
```

В DataGrip вам придется запустиь запрос дважды, 
так как первый раз он спросит точно ли вы хотите все удалить,
а второй раз уже удалит данные. 

## IN, BETWEEN, LIKE

`IN` - оператор для сравнения значения с несколькими значениями.

```sql

SELECT * FROM Students WHERE Id IN (1, 2, 3);
```

`BETWEEN` - оператор для сравнения значения в диапазоне.

```sql

SELECT * FROM Students WHERE Id BETWEEN 1 AND 3;
```

`LIKE` - оператор для сравнения значения с шаблоном.

```sql

SELECT * FROM Students WHERE Name LIKE N'A%';
```

[Более подробно про LIKE](https://www.w3schools.com/sql/sql_wildcards.asp)

## TOP

`TOP` - оператор для выбора определенного количества строк.

```sql

SELECT TOP 3 * FROM Students;
```

## CASE WHEN

```sql

SELECT
    Id,
    Name,
    Surname,
    BirthDate,
    CASE
        WHEN BirthDate < '2000-01-01' THEN N'Старше 24 лет'
        WHEN BirthDate >= '2000-01-01' AND BirthDate <= '2005-01-01' THEN N'От 19 до 24 лет'
        ELSE N'Младше 19 лет'
    END AS AgeCategory
FROM dbo.Students;

```

## IIF

```sql

SELECT
    Id,
    Name,
    Surname,
    BirthDate,
    IIF(BirthDate < '2000-01-01', N'Старше 24 лет', N'Младше 24 лет') AS AgeCategory
    FROM dbo.Students;

```

## Foreign Key

`Foreign Key` - внешний ключ, который связывает две таблицы.

Так как `SQL` - это реляционная база данных, то мы можем строить отношения 
между таблицами. Это нужно для того чтобы избежать дублирования данных.

Также вы можете это расценивать как агрегацию и композицию в ООП. Например, 
если у нас есть 3 класса как: Group, Student, Teacher, то мы можем 
связать их между собой.

```csharp

class Student 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public Group Group { get; set; }
}

class Group 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Student> Students { get; set; }
}

class Teacher 
{
    public int Id { get; set; }
    public string Name { get; set; }
    public List<Group> Groups { get; set; }
}

```

При создании внешнего ключа вы ссылаетесь на его `Primary Key`.

`Foreign Key` - можно создать как при создании таблицы, так и после.

```sql

CREATE TABLE Groups
(
    Id INT PRIMARY KEY,
    Name NVARCHAR(50)
);

CREATE TABLE Students
(
    Id INT PRIMARY KEY,
    Name NVARCHAR(50),
    GroupId INT FOREIGN KEY REFERENCES Groups(Id)
);

```

```sql

ALTER TABLE Students
ADD FOREIGN KEY (GroupId) REFERENCES Groups(Id);

```

## Нормализация таблиц

`Нормализация` - это процесс организации данных в базе данных.

Есть 6 нормальных форм:
1. 1NF - каждая ячейка содержит только одно значение
2. 2NF - каждая ячейка зависит от первичного ключа
3. 3NF - каждая ячейка зависит только от первичного ключа
4. Нормальная форма Бойса-Кодда
5. 4NF - каждая ячейка зависит только от первичного ключа
6. 5NF - каждая ячейка зависит только от первичного ключа

[Обязательно к прочтению](https://habr.com/ru/articles/254773/)

Чрезмерная нормализация базы данных приводит к ухудшению 
производительности и понимания структуры базы данных.

Даже есть такой термин как, `денормализация`, которую 
производят если база данных слишком нормализована и мы по сути
жертвуем красотой ради производительности.

## JOIN и UNION

`JOIN` - оператор для объединения таблиц.

Есть Inner Join, Left Join, Right Join, Full Join.

### Вот мой любимый мем с диаграммой Венна: 

![](https://media.licdn.com/dms/image/v2/D4D22AQHeU4OzQVrH1g/feedshare-shrink_2048_1536/feedshare-shrink_2048_1536/0/1685862307812?e=2147483647&v=beta&t=W9y6ZWSfV4gVlu9wAXy5X3J4m7lksjgC7W13_f5F4Nw)

### А вот крутая серьезная диаграмма:
![](https://cdn.analyticsvidhya.com/wp-content/uploads/2020/02/sql-joins-diagram.webp)

Вот пример с кодом для `Join` - ов:

```sql

SELECT
    Students.Id,
    Students.Name,
    Groups.Name AS GroupName
FROM Students
INNER JOIN Groups ON Students.GroupId = Groups.Id;

```

```sql
SELECT
    Students.Id,
    Students.Name,
    Groups.Name AS GroupName
FROM Students
LEFT JOIN Groups ON Students.GroupId = Groups.Id;

```


```sql
SELECT
    Students.Id,
    Students.Name,
    Groups.Name AS GroupName
FROM Students
LEFT JOIN Groups ON Students.GroupId = Groups.Id
WHERE Groups.Name IS NULL;

```

```sql
SELECT
    Students.Id,
    Students.Name,
    Groups.Name AS GroupName
FROM Students
FULL OUTER JOIN Groups ON Students.GroupId = Groups.Id;
```

`UNION` - оператор для объединения результатов запросов.

Для того чтобы он работал достаточно того чтобы данные были одного типа. 

```sql

SELECT Name FROM Students
UNION
SELECT Name FROM Teachers;

```

`UNION ALL` - оператор для объединения результатов запросов с дубликатами.


## Агрегатные функции

`Агрегатные функции` - это функции для вычисления суммы, среднего, минимального, максимального значения.

- `COUNT` - количество строк
- `SUM` - сумма значений
- `AVG` - среднее значение
- `MIN` - минимальное значение
- `MAX` - максимальное значение

```sql

SELECT COUNT(*) FROM Students;

```

```sql
SELECT MAX(BirthDate) FROM Students;
```

### HAVING и GROUP BY

`HAVING` - оператор для фильтрации групп.
`GROUP BY` - оператор для группировки строк.

Для того чтобы использовать `HAVING` нужно использовать `GROUP BY`, а 
для того чтобы использовать `GROUP BY` нужно использовать агрегатные функции.

```sql

SELECT
    COUNT(*),
    BirthDate,
    Name
FROM Students
GROUP BY BirthDate

```