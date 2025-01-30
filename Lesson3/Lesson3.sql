
IF NOT EXISTS(SELECT * FROM sys.databases WHERE name = 'Academy')
    CREATE DATABASE Academy

GO


USE Academy

CREATE Table Students
(
    [Id] int identity(1, 1) PRIMARY KEY,
    [Name] nvarchar(50) NOT NULL,
    [Surname] nvarchar(50) NOT NULL,
    [Email] nvarchar(50) NOT NULL UNIQUE,
    [BirthDate] date
);

GO

SELECT * FROM Students

GO
