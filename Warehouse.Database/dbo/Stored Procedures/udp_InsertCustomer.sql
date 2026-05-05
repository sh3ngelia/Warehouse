-- Customers

create procedure udp_InsertCustomer
    @CustomerType bit,
    @Phone char(12),
    @Email varchar(50),
    @CustomerId int output
as
begin
    set nocount on;

    begin try
        if @CustomerType is null
            raiserror('CustomerType is required.', 16, 1);

        -- Check uniqueness
        if exists (select 1 from Customers where Email = @Email and IsDeleted = 0)
        begin
            raiserror('Customer type with this Email already exists.', 16, 1);
            return 1;
        end

        if exists (select 1 from Customers where Phone = @Phone and IsDeleted = 0)
        begin
            raiserror('Customer type with this Phone already exists.', 16, 1);
            return 1;
        end

        begin tran;

        insert into Customers(CustomerType, Phone, Email)
        values (@CustomerType, @Phone, @Email);
        set @CustomerId = scope_identity();

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end