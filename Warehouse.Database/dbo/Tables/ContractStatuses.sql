-- Modified to tinyint
create table ContractStatuses
(
    ContractStatusId tinyint primary key identity (1, 1),
    Name varchar(15) not null,
    CreateDate datetime not null default(getdate()),
    UpdateDate datetime null,
    IsDeleted bit not null default(0)
);