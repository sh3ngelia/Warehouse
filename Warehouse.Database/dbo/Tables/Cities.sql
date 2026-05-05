create table Cities
(
    CityId int primary key identity (1, 1),
    RegionId int not null references Regions(RegionId),
    Name varchar(20) not null,
    CreateDate datetime not null default(getdate()),
    UpdateDate datetime null,
    IsDeleted bit not null default(0)
);