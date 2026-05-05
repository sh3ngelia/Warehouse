-- Physicalcustomers

create procedure udp_InsertPhysicalCustomer

    @FirstName varchar(20),
    @LastName varchar(20),
    @PersonalId char(11),
    @CustomerId int output
as
begin
    set nocount on;

    begin try

        if @FirstName is null or ltrim(rtrim(@FirstName)) = ''
            raiserror('FirstName cannot be empty.', 16, 1);

        if @LastName is null or ltrim(rtrim(@LastName)) = ''
            raiserror('LastName cannot be empty.', 16, 1);

        if @PersonalId is null or ltrim(rtrim(@PersonalId)) = ''
            raiserror('PersonalId cannot be empty.', 16, 1);

        -- Check PersonalId uniqueness
        if exists (select 1 
                   from PhysicalCustomers pc 
                   join Customers c on pc.CustomerId = c.CustomerId 
                   where pc.PersonalId = @PersonalId and c.IsDeleted = 0)
        begin
            raiserror('Physical customer with this PersonalId already exists.', 16, 1);
            return 1;
        end

        begin tran;

        insert into PhysicalCustomers(FirstName, LastName, PersonalId)
        values (@FirstName, @LastName, @PersonalId);
        set @CustomerId = scope_identity();

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end