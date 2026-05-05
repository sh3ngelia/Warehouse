-- CRUD Procedures

-- Roles
create procedure udp_InsertRole
    @Name varchar(20),
    @RoleId int output
as
begin
    set nocount on;

    begin try        
        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Role name cannot be empty.', 16, 1);      
        -- Check Name uniqueness
        if exists (select 1 from Roles where Name = @Name and IsDeleted = 0)
        begin
            raiserror('Role with this Name already exists.', 16, 1);
            return 1;
        end

        begin tran;

        insert into Roles([Name])
        values (ltrim(rtrim(@Name)));
        set @RoleId = scope_identity();

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end