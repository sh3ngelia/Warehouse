create table ContractDetails
(
    ContractDetailId int primary key identity(1,1),
    ContractId int not null references Contracts(ContractId),
    StorageId int not null references Storages(StorageId),
    Price money check (Price >= 0)
);