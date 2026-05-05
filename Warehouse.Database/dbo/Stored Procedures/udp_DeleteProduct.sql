create procedure udp_DeleteProduct
    @ProductId int
as
begin
    set nocount on;

    begin try
        if @ProductId is null or @ProductId <= 0
            raiserror('ProductId must be a positive integer.', 16, 1);

        begin tran;

        update Products
        set IsDeleted = 1,
            UpdateDate = getdate()
        where ProductId = @ProductId
          and IsDeleted = 0;

        if @@rowcount = 0
            raiserror('Product not found.', 16, 1);

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end