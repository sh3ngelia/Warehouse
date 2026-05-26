create procedure udp_DeleteLegalCustomer
    @LegalCustomerId int
as
begin
    set nocount on;

    begin try
        if @LegalCustomerId is null or @LegalCustomerId <= 0
            raiserror('CustomerId must be a positive integer.', 16, 1);

        if not exists (select 1 from LegalCustomers where CustomerId = @LegalCustomerId)
            raiserror('Legal customer not found.', 16, 1);

        begin tran;

        delete from LegalCustomers
        where CustomerId = @LegalCustomerId;

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end