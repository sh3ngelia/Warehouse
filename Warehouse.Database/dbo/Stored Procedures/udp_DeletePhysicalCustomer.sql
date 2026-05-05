create procedure udp_DeletePhysicalCustomer
    @CustomerId int
as
begin
    set nocount on;

    begin try
        if @CustomerId is null or @CustomerId <= 0
            raiserror('CustomerId must be a positive integer.', 16, 1);

        if not exists (select 1 from PhysicalCustomers where CustomerId = @CustomerId)
            raiserror('Physical customer not found.', 16, 1);

        begin tran;

        delete from PhysicalCustomers
        where CustomerId = @CustomerId;

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end