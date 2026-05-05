-- ContractDetails

create procedure udp_InsertContractDetail
    @ContractId int,
    @StorageId int,
    @Price money,
    @ContractDetailId int output

as
begin
    set nocount on;

    begin try
        if @ContractId is null or @ContractId <= 0
            raiserror('ContractId must be a positive integer.', 16, 1);

        if @StorageId is null or @StorageId <= 0
            raiserror('StorageId must be a positive integer.', 16, 1);

        if @Price is null or @Price < 0
            raiserror('Price must be >= 0.', 16, 1);

        if not exists (select 1 from Contracts where ContractId = @ContractId)
            raiserror('Contract not found.', 16, 1);

        if not exists (select 1 from Storages where StorageId = @StorageId and IsDeleted = 0)
            raiserror('Storage not found.', 16, 1);

        begin tran;

        insert into ContractDetails(ContractId, StorageId, Price)
        values (@ContractId, @StorageId, @Price);
        set @ContractDetailId = scope_identity();

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end