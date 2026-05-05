create procedure udp_UpdateLegalCustomer
    @CustomerId int,
    @Name nvarchar(30),
    @Address varchar(100)
as
begin
    set nocount on;

    begin try
        if @CustomerId is null or @CustomerId <= 0
            raiserror('CustomerId must be a positive integer.', 16, 1);

        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Legal customer name cannot be empty.', 16, 1);

        if not exists (select 1 from LegalCustomers where CustomerId = @CustomerId)
            raiserror('Legal customer not found.', 16, 1);

        -- Check Phone uniqueness (excluding current record)
        if exists (select 1 from LegalCustomers lc
                   join Customers c on lc.CustomerId = c.CustomerId
                   where lc.Name = @Name and lc.CustomerId != @CustomerId and c.IsDeleted = 0)
        begin
            raiserror('Customer with this Phone already exists.', 16, 1);
            return 1;
        end

        begin tran;

        update LegalCustomers
        set [Name] = @Name,
            [Address] = @Address
        where CustomerId = @CustomerId;

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end