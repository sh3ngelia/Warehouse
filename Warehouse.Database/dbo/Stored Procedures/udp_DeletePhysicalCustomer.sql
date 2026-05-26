create procedure udp_DeletePhysicalCustomer
    @PhysicalCustomerId int
as
begin
    set nocount on;

    begin try
        if @PhysicalCustomerId is null or @PhysicalCustomerId <= 0
            raiserror('CustomerId must be a positive integer.', 16, 1);

        if not exists (select 1 from PhysicalCustomers where CustomerId = @PhysicalCustomerId)
            raiserror('Physical customer not found.', 16, 1);

        begin tran;

        delete from PhysicalCustomers
        where CustomerId = @PhysicalCustomerId;

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end