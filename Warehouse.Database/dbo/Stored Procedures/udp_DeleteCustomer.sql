create procedure udp_DeleteCustomer
    @CustomerId int
as
begin
    set nocount on;

    begin try
        if @CustomerId is null or @CustomerId <= 0
            raiserror('CustomerId must be a positive integer.', 16, 1);

        begin tran;

        update Customers
        set IsDeleted = 1,
            UpdateDate = getdate()
        where CustomerId = @CustomerId
          and IsDeleted = 0;

        if @@rowcount = 0
            raiserror('Customer not found.', 16, 1);

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end