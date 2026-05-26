create procedure udp_CreateTestEmployeesAndUsers
    as
begin
    set nocount on;

    declare @i int = 1;
    declare @EmployeeId int;

    while @i <= 10
begin
        -- Insert Employee
insert into Employees
(
    PersonalId,
    FirstName,
    LastName,
    Phone,
    Email
)
values
    (
        right('00000000000' + cast(@i as varchar(11)), 11),
              concat('FirstName', @i),
              concat('LastName', @i),
              concat('55500000', right('00' + cast(@i as varchar),2)),
              concat('user', @i, '@mail.com')
    );

-- Get generated EmployeeId
set @EmployeeId = scope_identity();

        -- Insert User for that Employee
insert into Users
(
    EmployeeId,
    Username,
    Password
)
values
    (
        @EmployeeId,
        concat('user', @i),
        hashbytes('SHA2_256', concat('Password', @i))
    );

set @i = @i + 1;
end
end