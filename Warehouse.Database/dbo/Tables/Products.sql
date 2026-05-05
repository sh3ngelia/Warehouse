create table Products
(
    ProductId int primary key identity(1, 1),
    CategoryId int not null references Categories(CategoryId),
    Name varchar(100) not null,
    SKU char(20) not null,
    Description varchar(max),
    CreateDate datetime not null default(getdate()),
    UpdateDate datetime null,
    IsDeleted bit not null default(0)
);