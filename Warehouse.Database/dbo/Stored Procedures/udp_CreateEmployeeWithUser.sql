create procedure udp_CreateEmployeeWithUser
    @PersonalId       varchar(20),
    @FirstName        varchar(50),
    @LastName         varchar(50),
    @Phone            varchar(20),
    @Email            varchar(100),

    @UserRoleId       int,
    @Username         varchar(50),
    @PasswordHash     varbinary(256),

    @NewEmployeeId    int output
as
begin
    set nocount on;

    begin try
        begin tran;

        insert into Employees(PersonalId, FirstName, LastName, Phone, Email)
        values(@PersonalId, @FirstName, @LastName, @Phone, @Email);

        set @NewEmployeeId = scope_identity();

        insert into Users(EmployeeId, Username, [Password])
        values(@NewEmployeeId, @Username, @PasswordHash);

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end