create procedure udp_InsertEmployee
    @EmployeeId int output,
    @PersonalId char(11),
    @FirstName nvarchar(20),
    @LastName nvarchar(30),
    @Phone char(12),
    @Email nvarchar(50)
as
begin
    set nocount on;

begin try
if @PersonalId is null or ltrim(rtrim(@PersonalId)) = ''
            raiserror('PersonalId cannot be empty.', 16, 1);

        if @FirstName is null or ltrim(rtrim(@FirstName)) = ''
            raiserror('FirstName cannot be empty.', 16, 1);

        if @LastName is null or ltrim(rtrim(@LastName)) = ''
            raiserror('LastName cannot be empty.', 16, 1);

        if exists (select 1 from Employees where PersonalId = @PersonalId and IsDeleted = 0)
begin
            raiserror('Employee with this PersonalId already exists.', 16, 1);
return 1;
end

        if exists (select 1 from Employees where Email = @Email and IsDeleted = 0)
begin
            raiserror('Employee with this Email already exists.', 16, 1);
return 1;
end

begin tran;

insert into Employees(PersonalId, FirstName, LastName, Phone, Email)
values (@PersonalId, @FirstName, @LastName, @Phone, @Email);

set @EmployeeId = scope_identity();

commit;
return 0;
end try
begin catch
if @@trancount > 0 rollback;
        throw;
end catch
end