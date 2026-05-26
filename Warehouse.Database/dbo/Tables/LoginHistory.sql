create table LoginHistory
(
    LoginHistoryId int primary key identity(1, 1),
    UserId int not null references Users(EmployeeId),
    LoginAt datetime not null default(getdate()),
    LogoutAt datetime null,
);