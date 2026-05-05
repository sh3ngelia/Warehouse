create procedure udp_DeleteContractStatus
    @ContractStatusId tinyint
as
begin
    set nocount on;

    begin try
        if @ContractStatusId is null or @ContractStatusId <= 0
            raiserror('ContractStatusId must be a positive integer.', 16, 1);

        begin tran;

        update ContractStatuses
        set IsDeleted = 1,
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