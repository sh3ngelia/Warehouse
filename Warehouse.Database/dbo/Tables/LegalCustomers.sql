create table LegalCustomers
(
    CustomerId int primary key references Customers(CustomerId),
    Name nvarchar(30) not null,
    Address varchar(100) not null
);