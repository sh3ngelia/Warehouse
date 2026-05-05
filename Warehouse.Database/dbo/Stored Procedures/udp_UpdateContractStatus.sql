create procedure udp_UpdateContractStatus
    @ContractStatusId tinyint,
    @Name varchar(15)
as
begin
    set nocount on;

    begin try
        if @ContractStatusId is null or @ContractStatusId <= 0
            raiserror('ContractStatusId must be a positive integer.', 16, 1);

        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Contract status name cannot be empty.', 16, 1);

        -- Check Name uniqueness (excluding current record)
        if exists (select 1 from ContractStatuses where Name = @Name and ContractStatusId != @ContractStatusId and IsDeleted = 0)
        begin
            raiserror('Contract status with this Name already exists.', 16, 1);
            return 1;
        end

        begin tran;

        update ContractStatuses
        set [Name] = ltrim(rtrim(@Name)),
            UpdateDate = getdate()
        where ContractStatusId = @ContractStatusId
          and IsDeleted = 0;

        if @@rowcount = 0
            raiserror('Contract status not found.', 16, 1);

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end