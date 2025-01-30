-- use Academy
-- go
--
-- insert into dbo.Students (Name, Surname, Email, BirthDate)
-- values
--     (N'Алексей', N'Иванов', N'alex.ivanov@email.com', '2000-05-14'),
--     (N'Мария', N'Петрова', N'maria.petrov@email.com', '1999-09-23'),
--     (N'Дмитрий', N'Сидоров', N'd.sidorov@email.com', '2001-03-11'),
--     (N'Ольга', N'Кузнецова', N'olga.kuznetsova@email.com', '2002-07-19'),
--     (N'Иван', N'Смирнов', N'ivan.smirnov@email.com', '2000-12-30'),
--     (N'Екатерина', N'Федорова', N'katya.fedorova@email.com', '1998-11-05'),
--     (N'Сергей', N'Васильев', N'sergey.vasilev@email.com', '2001-01-27'),
--     (N'Анна', N'Михайлова', N'anna.mihailova@email.com', '1999-06-15'),
--     (N'Павел', N'Зайцев', N'pavel.zaitsev@email.com', '2002-04-02'),
--     (N'Татьяна', N'Борисова', N'tanya.borisova@email.com', '2000-08-09')
-- go
--
--
-- select * from Students;
--
--
-- select Name as 'Имя', Surname as 'Фамилия', Email as 'Электронная почта', BirthDate as 'Дата рождения'
-- from Students;
--
--
-- select top 3 * from Students
--
--
-- SELECT
--     Id,
--     Name,
--     Surname,
--     BirthDate,
--     CASE
--         WHEN BirthDate < '2000-01-01' THEN N'Старше 24 лет'
--         WHEN BirthDate >= '2000-01-01' AND BirthDate <= '2005-01-01' THEN N'От 19 до 24 лет'
--         ELSE N'Младше 19 лет'
--     END AS AgeCategory
-- FROM dbo.Students;
--
--
-- go;
--
--
-- SELECT
--     Id,
--     Name,
--     Surname,
--     BirthDate,
--     IIF(BirthDate < '2000-01-01', N'Старше 24 лет', N'Младше 24 лет') AS AgeCategory
-- FROM dbo.Students;
--
--
--

use Academy;

alter table Student
add GroupId int foreign key references [Group] (Id);

UPDATE Student SET GroupId = 1 WHERE Id = 1; -- Студент 1 в группе IT-101
UPDATE Student SET GroupId = 2 WHERE Id = 2; -- Студент 2 в группе IT-102
UPDATE Student SET GroupId = 3 WHERE Id = 3; -- Студент 3 в группе MATH-201


select People.Name as StudentName,
         People.Surname as StudentSurname,
        [Group].GroupNumber as GroupNumber,
            [Faculty].FacultyName as FacultyName
       from Student
inner join People on Student.Id = People.Id
inner join [Group] on Student.GroupId = [Group].Id
inner join Faculty on Faculty.Id = [Group].FacultyId
where YEAR(BirthDate) > 2000;





