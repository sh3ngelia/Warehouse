create procedure udp_InsertLegalCustomer
    @CustomerId int output,
    @Name nvarchar(30),
    @Address varchar(100)
    as
begin
    set nocount on;

begin try
    if @CustomerId is null
            raiserror('CustomerId cannot be null.', 16, 1);

    if @Name is null or ltrim(rtrim(@Name)) = ''
        raiserror('Legal customer name cannot be empty.', 16, 1);

    if @Address is null or ltrim(rtrim(@Address)) = ''
        raiserror('Legal customer address cannot be empty.', 16, 1);

begin tran;

insert into dbo.LegalCustomers (CustomerId, [Name], [Address])
values (@CustomerId, @Name, @Address);

commit;
end try
begin catch
if @@trancount > 0 rollback;
        throw;
end catch
end