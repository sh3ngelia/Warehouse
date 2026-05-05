create procedure udp_UpdateRole
    @RoleId int,
    @Name varchar(20)
as
begin
    set nocount on;

    begin try
        if @RoleId is null or @RoleId <= 0
            raiserror('RoleId must be a positive integer.', 16, 1);

        if @Name is null or ltrim(rtrim(@Name)) = ''
            raiserror('Role name cannot be empty.', 16, 1);

            -- Check Name uniqueness (excluding current record)
        if exists (select 1 from Roles where Name = @Name and RoleId != @RoleId and IsDeleted = 0)
        begin
            raiserror('Role with this Name already exists.', 16, 1);
            return 1;
        end;

        begin tran;

        update Roles
        set [Name] = ltrim(rtrim(@Name)),
            UpdateDate = getdate()
        where RoleId = @RoleId
          and IsDeleted = 0;

        if @@rowcount = 0
            raiserror('Role not found.', 16, 1);

        commit;
        return 0;
    end try
    begin catch
        if @@trancount > 0 rollback;
        throw;
    end catch
end