
-- Создание таблицы People
CREATE TABLE People
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Name NVARCHAR(50) NOT NULL,
    Surname NVARCHAR(50) NOT NULL,
    BirthDate DATE NOT NULL CHECK (YEAR(GETDATE()) - YEAR(BirthDate) >= 15)
);

-- Создание таблицы Faculty
CREATE TABLE Faculty
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FacultyName NVARCHAR(50) NOT NULL
);

-- Создание таблицы Group
CREATE TABLE [Group]
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    GroupNumber NVARCHAR(50) NOT NULL,
    FacultyId INT FOREIGN KEY REFERENCES Faculty(Id) on DELETE CASCADE
);

-- On DELETE CASCADE - при удалении факультета, удаляются все группы, которые к нему относятся

-- Создание таблицы Position
CREATE TABLE Position
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PositionName NVARCHAR(50) NOT NULL
);

-- Создание таблицы Subject
CREATE TABLE Subject
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    SubjectName NVARCHAR(50) NOT NULL
);

-- Создание таблицы Student
CREATE TABLE Student
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentNumber NVARCHAR(50) NOT NULL,
    PersonId INT FOREIGN KEY REFERENCES People(Id)
);

-- Создание таблицы Teacher
CREATE TABLE Teacher
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    TeacherNumber NVARCHAR(50) NOT NULL,
    PersonId INT FOREIGN KEY REFERENCES People(Id),
    Subject NVARCHAR(50) NOT NULL,
    GroupId INT FOREIGN KEY REFERENCES [Group](Id)
);


-- Создание таблицы Staff
CREATE TABLE Staff
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PositionId INT FOREIGN KEY REFERENCES Position(Id),
    PersonId INT FOREIGN KEY REFERENCES People(Id)
);

-- Создание таблицы GroupCurator
CREATE TABLE GroupCurator
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    PersonId INT FOREIGN KEY REFERENCES People(Id),
    GroupId INT FOREIGN KEY REFERENCES [Group](Id)
);

-- Создание таблицы StudentPoint
CREATE TABLE StudentPoint
(
    Id INT IDENTITY(1,1) PRIMARY KEY,
    StudentId INT FOREIGN KEY REFERENCES Student(Id),
    SubjectId INT FOREIGN KEY REFERENCES Subject(Id),
    Point INT NOT NULL CHECK (Point >= 1 AND Point <= 12)
);