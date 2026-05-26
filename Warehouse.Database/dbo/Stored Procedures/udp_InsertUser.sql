create procedure udp_InsertUser
    @EmployeeId int output,
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

        if @EmployeeId is not null and not exists (select 1 from dbo.Employees where EmployeeId = @EmployeeId and IsDeleted = 0)
            raiserror('Employee with this EmployeeId does not exist.', 16, 1);

        if exists (select 1 from dbo.Users where Username = @Username and IsDeleted = 0)
            raiserror('Username already exists.', 16, 1);

insert into dbo.Users (EmployeeId, Username, Password)
values (@EmployeeId, @Username, @Password);

end try
begin catch
throw;
end catch
end