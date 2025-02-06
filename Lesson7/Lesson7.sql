use Academy;

CREATE TRIGGER CheckStudentsCount on Student
FOR INSERT
AS
BEGIN
    IF (SELECT COUNT(*) FROM Student
        INNER JOIN [Group] on [Group].Id = Student.GroupId) >= 30
        BEGIN
            print(N'Student limit !');
            ROLLBACK TRANSACTION;
        END
END
go




CREATE TRIGGER CheckGroupStudentsCount on Student
FOR INSERT, UPDATE
AS
    BEGIN

        DECLARE @GroupId int;
        SET @GroupId = (SELECT GroupId FROM inserted);

        IF (select COUNT(*) as GroupCount  from Student
            inner join dbo.[Group] G on G.Id = Student.GroupId
            where G.Id = @GroupId
            group by G.Id)
            >= 5
        BEGIN
            print(N'Group is full!');
            ROLLBACK TRANSACTION;
        end
    end


select * from People;

insert into People values ('John', 'Doe', '1990-01-01');
insert into People values ('Jane', 'Doe', '1990-01-01');
insert into People values ('Jack', 'Doe', '1990-01-01');
insert into People values ('Jill', 'Doe', '1990-01-01');

insert into Student (StudentNumber, PersonId, GroupId) values ('S12348', 6, 1);

insert into Student (StudentNumber, PersonId, GroupId) values ('S12349', 10, 1)
insert into Student (StudentNumber, PersonId, GroupId) values ('S12350',11, 1)
insert into Student (StudentNumber, PersonId, GroupId) values ('S12351', 12, 2)


select * from Student;

select G.Id, COUNT(*) as GroupCount from Student
    inner join dbo.[Group] G on G.Id = Student.GroupId
    group by G.Id
    having COUNT(*) >= 5;


-- declare @studentsSum int;
-- set @studentsSum = (select COUNT(*) from Student);
--
-- if @studentsSum >= 30
-- begin
--     print(N'Student limit !');
-- end




