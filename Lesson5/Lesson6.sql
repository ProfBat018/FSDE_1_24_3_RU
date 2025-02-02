-- use Academy;
--
-- SELECT * FROM People, Subject;
--
-- SELECT Id, Name from People
-- UNION
-- SELECT Id, SubjectName from Subject;
--

SELECT * FROM Student
WHERE Student.Id = 3;


-- Group By clause

SELECT S.SubjectName, AVG(SP.Point) as AveragePoint FROM dbo.Student
LEFT JOIN dbo.People P on P.Id = dbo.Student.PersonId
LEFT JOIN dbo.StudentPoint SP on dbo.Student.Id = SP.StudentId
LEFT join dbo.Subject S on S.Id = SP.SubjectId
GROUP BY S.SubjectName

INSERT INTO People (Name, Surname, BirthDate)
VALUES (N'Elvin', N'Azimov', '2001-11-16'),
       (N'Saleh', N'Cəbiyev', '1993-08-30');

select * from People

INSERT INTO Student(StudentNumber, PersonId, GroupId)
VALUES ('123456', 6, 1),
       ('123457', 7, 1);

select * from Student

select * from StudentPoint

INSERT INTO StudentPoint(StudentId, SubjectId, Point)
VALUES (4, 1, 99),
       (4, 2, 99),
       (4, 3, 99);


INSERT INTO StudentPoint(StudentId, SubjectId, Point)
VALUES (5, 1, 100),
       (5, 2, 100);


select p.Name, AVG(SP.Point) as AvgPoint from dbo.Student
inner join dbo.StudentPoint SP on dbo.Student.Id = SP.StudentId
inner join dbo.Subject S on S.Id = SP.SubjectId
inner join dbo.People P on P.Id = Student.PersonId
group by p.Name


select p.Name, s.SubjectName, AVG(sp.Point) as AvgPoint from dbo.Student
inner join dbo.StudentPoint SP on dbo.Student.Id = SP.StudentId
inner join dbo.Subject S on S.Id = SP.SubjectId
inner join dbo.People P on P.Id = Student.PersonId
group by p.Name, s.SubjectName
having AVG(SP.Point) >= 91
order by AvgPoint desc


-- subquery

select p.Name, s.SubjectName, s.Id, AVG(sp.Point) as AvgPoint from dbo.Student
inner join dbo.StudentPoint SP on dbo.Student.Id = SP.StudentId
inner join dbo.Subject S on S.Id = SP.SubjectId
inner join dbo.People P on P.Id = Student.PersonId
where SP.Point >= (select AVG(Point) from StudentPoint)
group by p.Name, s.SubjectName, s.Id


-- INSERT INTO Teacher(TeacherNumber, PersonId, Subject)
-- values (select N'T' + CONVERT(nvarchar(max), p.Id) + ' - ' + p.Name as TeacherNum, s.SubjectName, s.Id, AVG(sp.Point) as AvgPoint
-- from dbo.Student
-- inner join dbo.StudentPoint SP on dbo.Student.Id = SP.StudentId
-- inner join dbo.Subject S on S.Id = SP.SubjectId
-- inner join dbo.People P on P.Id = Student.PersonId
-- where SP.Point >= (select AVG(Point) from StudentPoint)
-- group by p.Id, p.Name, s.SubjectName, s.Id )



alter table Teacher
add SubjectId int foreign key references Subject(Id)

update Teacher
set Teacher.SubjectId = (
select Id from Subject
    where Subject.SubjectName = Teacher.Subject
    )
where Teacher.SubjectId is null


