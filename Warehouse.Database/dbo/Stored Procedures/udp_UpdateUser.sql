create procedure udp_UpdateUser
    @EmployeeId int,
    @Username varchar(40),
    @Password varbinary(256)
as
begin
    set nocount on;

begin try
if @EmployeeId is null
            raiserror('EmployeeId cannot be null.', 16, 1);

        if @Username is null or ltrim(rtrim(@Username)) = ''
            raiserror('Username cannot be empty.', 16, 1);

        if @Password is null
            raiserror('Password cannot be null.', 16, 1);

        if not exists (select 1 from dbo.Users where EmployeeId = @EmployeeId)
            raiserror('User not found.', 16, 1);

        if exists (
            select 1 from dbo.Users
            where Username = @Username
              and EmployeeId <> @EmployeeId
              and IsDeleted = 0
        )
            raiserror('Username already exists.', 16, 1);

update dbo.Users
set Username = @Username,
    Password = @Password,
    UpdateDate = getdate()
where EmployeeId = @EmployeeId;
end try
begin catch
throw;
end catch
end