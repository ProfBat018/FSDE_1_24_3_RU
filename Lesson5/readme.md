# Темы урока:
- Декартово произведение
- P.S. Truncate из DDL
- P.S. Alter из DDL
- Having, Group By, Order By
- Подзапросы

# Декартово произведение

Декартово произведение - это операция, 
которая объединяет все строки одной таблицы со всеми строками другой таблицы.

Пример:

```sql

SELECT * FROM People, Subject;

```

# P.S. Truncate из DDL

Truncate - это операция, которая удаляет все строки из таблицы.

Пример:

```sql

TRUNCATE TABLE People;

```

# P.S. Alter из DDL

Alter - это операция, которая изменяет структуру таблицы.

Пример:

```sql

ALTER TABLE People ADD COLUMN Age INT;

alter table Student
add GroupId int foreign key references [Group] (Id);

```

# Group By, Having, Order By

`Group By` - это операция, которая группирует строки по одному или нескольким столбцам.
`Having` - это операция, которая фильтрует группы.
`Order By` - это операция, которая сортирует строки.

Group By используется в паре с агрегатными функциями, такими как COUNT, SUM, AVG, MAX, MIN.

Примеры в файле `Lesson6.sql`

# Подзапросы

Подзапрос - это запрос внутри другого запроса.




