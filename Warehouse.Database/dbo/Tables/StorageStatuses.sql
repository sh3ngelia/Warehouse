create table StorageStatuses
(
    StorageStatusId int primary key identity (1, 1),
    Name varchar(15) not null,
    CreateDate datetime not null default(getdate()),
    UpdateDate datetime null,
    IsDeleted bit not null default(0)
);