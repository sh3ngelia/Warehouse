create table Storages
(
    StorageId int primary key identity(1, 1),
    Status int not null references StorageStatuses(StorageStatusID),
    CityId int not null references Cities(CityId),
    Name varchar(20) not null,
    Description nvarchar(max),
    Capacity float not null, -- Unit - 1 m³
    Address nvarchar(250) not null, -- changed
    Price money not null, -- Unit - Price per 1 m³
    CreateDate datetime not null default(getdate()),
    UpdateDate datetime null,
    IsDeleted bit not null default(0)
);