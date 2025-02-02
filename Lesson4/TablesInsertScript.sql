use Academy_2;
go;

-- Заполняем People
INSERT INTO People (Name, Surname, BirthDate) VALUES
(N'Алексей', N'Иванов', '1985-05-14'),
(N'Мария', N'Петрова', '1990-09-23'),
(N'Дмитрий', N'Сидоров', '2001-03-11'),
(N'Ольга', N'Кузнецова', '2002-07-19'),
(N'Иван', N'Смирнов', '2000-12-30');

-- Заполняем Faculty
INSERT INTO Faculty (FacultyName) VALUES
(N'Факультет информационных технологий'),
(N'Факультет математики');

-- Заполняем Group
INSERT INTO [Group] (GroupNumber, FacultyId) VALUES
(N'IT-101', 1),
(N'IT-102', 1),
(N'MATH-201', 2);

-- Заполняем Position
INSERT INTO Position (PositionName) VALUES
(N'Декан'),
(N'Заместитель декана'),
(N'Секретарь');

-- Заполняем Subject
INSERT INTO Subject (SubjectName) VALUES
(N'Математика'),
(N'Программирование'),
(N'Базы данных');

-- Заполняем Student
INSERT INTO Student (StudentNumber, PersonId) VALUES
(N'S12345', 3),
(N'S12346', 4),
(N'S12347', 5);

-- Заполняем Teacher
INSERT INTO Teacher (TeacherNumber, PersonId, Subject, GroupId) VALUES
(N'T-001', 1, N'Программирование', 1),
(N'T-002', 2, N'Математика', 3);

-- Заполняем Staff
INSERT INTO Staff (PositionId, PersonId) VALUES
(1, 1),
(2, 2);

-- Заполняем GroupCurator
INSERT INTO GroupCurator (PersonId, GroupId) VALUES
(1, 1),
(2, 3);

-- Заполняем StudentPoint
INSERT INTO StudentPoint (StudentId, SubjectId, Point) VALUES
(1, 1, 8),
(2, 2, 12),
(3, 3, 6);