-- StorageDetails

create procedure udp_InsertStorageDetail
    @ContractDetailId int,
    @ProductId int,
    @Quantity int,
    @StorageDetailId int output
as
begin
    set nocount on;

    begin try
        if @ContractDetailId is null or @ContractDetailId <= 0
            raiserror('ContractDetailId must be a positive integer.', 16, 1);

        if @ProductId is null or @ProductId <= 0
            raiserror('ProductId must be a positive integer.', 16, 1);

        if @Quantity is null or @Quantity <= 0
            raiserror('Quantity must be > 0.', 16, 1);

        if not exists (select 1 from ContractDetails where ContractDetailId = @ContractDetailId)
            raiserror('Contract detail not found.', 16, 1);

        if not exists (select 1 from Products where ProductId = @ProductId and IsDeleted = 0)
            raiserror('Product not found.', 16, 1);

        begin tran;

        insert into StorageDetails(ContractDetailId, ProductId, Quantity)
        values (@ContractDetailId, @ProductId, @Quantity);
        set @StorageDetailId = scope_identity();

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end