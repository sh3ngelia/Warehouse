create table Contracts
(
    ContractId int identity(1,1) primary key,
    CustomerId int not null references Customers(CustomerId),
    EmployeeId int not null references Employees(EmployeeId),
    ContractStatus tinyint not null references ContractStatuses(ContractStatusId),
    CreateDate datetime not null default(getdate())
);