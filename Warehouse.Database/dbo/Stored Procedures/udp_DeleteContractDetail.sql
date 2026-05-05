create procedure udp_DeleteContractDetail
    @ContractDetailId int
as
begin
    set nocount on;

    begin try
        if @ContractDetailId is null or @ContractDetailId <= 0
            raiserror('ContractDetailId must be a positive integer.', 16, 1);

        if not exists (select 1 from ContractDetails where ContractDetailId = @ContractDetailId)
            raiserror('Contract detail not found.', 16, 1);

        begin tran;

        delete from ContractDetails
        where ContractDetailId = @ContractDetailId;

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end