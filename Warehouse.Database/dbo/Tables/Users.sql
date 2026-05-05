create table Users
(
    EmployeeId int primary key references Employees(EmployeeId),
    Username varchar(40) not null,
    Password varbinary(256) not null,
    CreateDate datetime not null default(getdate()),
    UpdateDate datetime null,
    IsDeleted bit not null default(0)
);