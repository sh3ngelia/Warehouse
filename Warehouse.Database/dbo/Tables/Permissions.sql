create table Permissions
(
    PermissionId int primary key identity(1, 1),
    Name varchar(30) not null,
    PermissionKey smallint not null,
    Description varchar(max),
    CreateDate datetime not null default(getdate()),
    UpdateDate datetime null,
    IsDeleted bit not null default(0)
);