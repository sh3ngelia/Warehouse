create procedure udp_UpdatePhysicalCustomer
    @CustomerId int,
    @FirstName varchar(20),
    @LastName varchar(20),
    @PersonalId char(11)
as
begin
    set nocount on;

    begin try
        if @CustomerId is null or @CustomerId <= 0
            raiserror('CustomerId must be a positive integer.', 16, 1);

        if @FirstName is null or ltrim(rtrim(@FirstName)) = ''
            raiserror('FirstName cannot be empty.', 16, 1);

        if @LastName is null or ltrim(rtrim(@LastName)) = ''
            raiserror('LastName cannot be empty.', 16, 1);

        if @PersonalId is null or ltrim(rtrim(@PersonalId)) = ''
            raiserror('PersonalId cannot be empty.', 16, 1);

        if not exists (select 1 from PhysicalCustomers where CustomerId = @CustomerId)
            raiserror('Physical customer not found.', 16, 1);

        -- Check PersonalId uniqueness (excluding current record)
        if exists (select 1 
                   from PhysicalCustomers pc 
                   join Customers c on pc.CustomerId = c.CustomerId 
                   where pc.PersonalId = @PersonalId 
                     and pc.CustomerId != @CustomerId 
                     and c.IsDeleted = 0)
        begin
            raiserror('Physical customer with this PersonalId already exists.', 16, 1);
            return 1;
        end

        begin tran;

        update PhysicalCustomers
        set FirstName = @FirstName,
            LastName = @LastName,
            PersonalId = @PersonalId
        where CustomerId = @CustomerId;

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end