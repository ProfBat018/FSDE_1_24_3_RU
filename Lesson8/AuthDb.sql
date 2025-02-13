create database Auth_3;

go;

use Auth_3;

go;

create table Users
(
    userName nvarchar(50) primary key,
    password nvarchar(max) not null,
    email nvarchar(50) not null unique,
    isEmailConfirmed bit default 0
);

create table Roles
(
    roleName nvarchar(50) primary key,
);

create table UserRoles
(
    userRoleId int identity(1, 1) primary key,
    userNameRef nvarchar(50) not null foreign key references Users(username),
    roleNameRef nvarchar(50) not null foreign key references Roles(roleName)
);

go;



