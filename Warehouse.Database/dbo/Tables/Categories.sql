create table Categories
(
    CategoryId int primary key identity (1, 1),
    CategoryName varchar(20) not null,
    CreateDate datetime not null default(getdate()),
    UpdateDate datetime null,
    IsDeleted bit not null default(0)
);