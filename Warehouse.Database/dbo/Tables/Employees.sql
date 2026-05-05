-- Main Tables

create table Employees
(
    EmployeeId int primary key identity(1, 1),
    PersonalId char(11) not null, -- uniqueness will be in procedure
    FirstName nvarchar(20) not null,
    LastName nvarchar(30) not null,
    Phone char(12) not null,
    Email nvarchar(50) not null,
    CreateDate datetime not null default(getdate()),
    UpdateDate datetime null,
    IsDeleted bit not null default(0)
);