-- Contracts

create procedure udp_InsertContract
    @CustomerId int,
    @EmployeeId int,
    @ContractStatus tinyint,
    @ContractId int output
    as
begin
    set nocount on;

begin try
if @CustomerId is null or @CustomerId <= 0
            raiserror('CustomerId must be a positive integer.', 16, 1);

        if @EmployeeId is null or @EmployeeId <= 0
            raiserror('EmployeeId must be a positive integer.', 16, 1);

        if @ContractStatus is null or @ContractStatus <= 0
            raiserror('ContractStatus must be a positive integer.', 16, 1);

        if not exists (select 1 from Customers where CustomerId = @CustomerId and IsDeleted = 0)
            raiserror('Customer not found.', 16, 1);

        if not exists (select 1 from Employees where EmployeeId = @EmployeeId and IsDeleted = 0)
            raiserror('Employee not found.', 16, 1);

        if not exists (select 1 from ContractStatuses where ContractStatusId = @ContractStatus and IsDeleted = 0)
            raiserror('Contract status not found.', 16, 1);

begin tran;

insert into Contracts(CustomerId, EmployeeId, ContractStatus)
values (@CustomerId, @EmployeeId, @ContractStatus);

set @ContractId = scope_identity();

commit;
return 0;
end try
begin catch
if @@trancount > 0 rollback;
        throw;
end catch
end