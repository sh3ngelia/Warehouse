-- Contractstatuses

create procedure udp_InsertContractStatus
    @Name varchar(15),
    @ContractStatusId int output
as
begin
    set nocount on;

    -- Check Name uniqueness
    if exists (select 1 from ContractStatuses where Name = @Name and IsDeleted = 0)
    begin
        raiserror('Contract status with this Name already exists.', 16, 1);
        return 1;
    end

    begin try
        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Contract status name cannot be empty.', 16, 1);

        begin tran;

        insert into ContractStatuses([Name])
        values (ltrim(rtrim(@Name)));
        set @ContractStatusId = scope_identity();

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end