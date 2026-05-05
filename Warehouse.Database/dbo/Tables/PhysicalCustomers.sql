create table PhysicalCustomers
(
    CustomerId int primary key references Customers(CustomerId),
    FirstName varchar(20) not null,
    LastName varchar(20) not null,
    PersonalId char(11) not null
);