-- Legalcustomers

create procedure udp_InsertLegalCustomer
    @Name nvarchar(30),
    @Address varchar(100),
    @LegalCustomerId int output
as
begin
    set nocount on;

    begin try

        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Legal customer name cannot be empty.', 16, 1);

        if @Address is null or ltrim(rtrim(@Address)) = ''
            raiserror('Legal customer address cannot be empty.', 16, 1);

        if exists (select 1 from LegalCustomers lc
                   join Customers c on lc.CustomerId = c.CustomerId
                   where lc.Name = @Name and c.IsDeleted = 0)

        begin tran;

        insert into LegalCustomers ([Name], [Address])
        values (@Name, @Address);
        set @LegalCustomerId = scope_identity();

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end