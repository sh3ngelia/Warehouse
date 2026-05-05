create table StorageDetails
(
    StorageDetailId int primary key identity(1,1),
    ContractDetailId int references ContractDetails(ContractDetailId),
    ProductId int not null references Products(ProductId),
    Quantity int not null check(Quantity >= 0)
);