create table Customers
(
    CustomerId int primary key identity(1, 1),
    CustomerType bit not null, -- 0: Physical, 1: Legal
    Phone char(12) not null,
    Email varchar(50), -- uniqueness will be in procedure
    CreateDate datetime not null default(getdate()),
    UpdateDate datetime null,
    IsDeleted bit not null default(0)
);