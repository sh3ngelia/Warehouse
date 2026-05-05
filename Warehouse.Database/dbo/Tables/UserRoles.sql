create table UserRoles
(
    UserId int not null references Users(EmployeeId),
    RoleId int not null references Roles(RoleId),
    primary key (UserId, RoleId)
);