create procedure udp_UpdateCustomer
    @CustomerId int,
    @CustomerType bit,
    @Phone char(12),
    @Email varchar(50)
as
begin
    set nocount on;

    begin try
        if @CustomerId is null or @CustomerId <= 0
            raiserror('CustomerId must be a positive integer.', 16, 1);

        if @CustomerType is null
            raiserror('CustomerType is required.', 16, 1);

        -- Check Name uniqueness (excluding current record)
        if exists (select 1 from Customers where Email = @Email and CustomerId != @CustomerId and IsDeleted = 0)
        begin
            raiserror('Customer type with this Email already exists.', 16, 1);
            return 1;
        end

        if exists (select 1 from Customers where Phone = @Phone and CustomerId != @CustomerId and IsDeleted = 0)
        begin
            raiserror('Customer type with this Phone already exists.', 16, 1);
            return 1;
        end

        begin tran;

        update Customers
        set CustomerType = @CustomerType,
            Phone = @Phone,
            Email = @Email,
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