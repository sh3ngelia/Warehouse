create procedure udp_DeleteContract
    @ContractId int
as
begin
    set nocount on;

    begin try
        if @ContractId is null or @ContractId <= 0
            raiserror('ContractId must be a positive integer.', 16, 1);

        if not exists (select 1 from Contracts where ContractId = @ContractId)
            raiserror('Contract not found.', 16, 1);

        begin tran;

        delete from Contracts
        where ContractId = @ContractId;

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end