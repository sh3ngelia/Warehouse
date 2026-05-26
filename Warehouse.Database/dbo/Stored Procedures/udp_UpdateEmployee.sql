create procedure udp_UpdateEmployee
    @EmployeeId int,
    @PersonalId char(11),
    @FirstName nvarchar(20),
    @LastName nvarchar(30),
    @Phone char(12),
    @Email nvarchar(50)
as
begin
    set nocount on;

begin try
if @EmployeeId is null or @EmployeeId <= 0
            raiserror('EmployeeId must be a positive integer.', 16, 1);

        if @PersonalId is null or ltrim(rtrim(@PersonalId)) = ''
            raiserror('PersonalId cannot be empty.', 16, 1);

        if @FirstName is null or ltrim(rtrim(@FirstName)) = ''
            raiserror('FirstName cannot be empty.', 16, 1);

        if @LastName is null or ltrim(rtrim(@LastName)) = ''
            raiserror('LastName cannot be empty.', 16, 1);

        if not exists (select 1 from Employees where EmployeeId = @EmployeeId and IsDeleted = 0)
            raiserror('Employee not found.', 16, 1);

        if exists (select 1 from Employees where PersonalId = @PersonalId and EmployeeId != @EmployeeId and IsDeleted = 0)
begin
            raiserror('Employee with this PersonalId already exists.', 16, 1);
return 1;
end

        if exists (select 1 from Employees where Email = @Email and EmployeeId != @EmployeeId and IsDeleted = 0)
begin
            raiserror('Employee with this Email already exists.', 16, 1);
return 1;
end

begin tran;

update Employees
set PersonalId = @PersonalId,
    FirstName = @FirstName,
    LastName = @LastName,
    Phone = @Phone,
    Email = @Email,
    UpdateDate = getdate()
where EmployeeId = @EmployeeId
  and IsDeleted = 0;

if @@rowcount = 0
            raiserror('Employee not found.', 16, 1);

commit;
return 0;
end try
begin catch
if @@trancount > 0 rollback;
        throw;
end catch
end