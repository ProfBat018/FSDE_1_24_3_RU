BEGIN TRANSACTION Test;

insert into People(Name, Surname, BirthDate)
values('John', 'Doe', '1990-01-01');


select * from People

COMMIT TRANSACTION Test;

ROLLBACK TRANSACTION Test;

alter table Teacher
add SubjectId int foreign key references Subject(Id)


select p.Name, s.SubjectName, s.Id, AVG(sp.Point) as AvgPoint from dbo.Student
inner join dbo.StudentPoint SP on dbo.Student.Id = SP.StudentId
inner join dbo.Subject S on S.Id = SP.SubjectId
 join dbo.People P on P.Id = Student.PersonId
where SP.Point >= (select AVG(Point) from StudentPoint)
group by p.Name, s.SubjectName, s.Id


select * from Teacher

update Teacher
set Teacher.SubjectId = (
select Id from Subject
    where Subject.SubjectName = Teacher.Subject
    )
where Teacher.SubjectId is null