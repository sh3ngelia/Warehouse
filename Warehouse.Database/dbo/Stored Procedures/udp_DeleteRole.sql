create procedure udp_DeleteRole
    @RoleId int
as
begin
    set nocount on;

    begin try
        if @RoleId is null or @RoleId <= 0
            raiserror('RoleId must be a positive integer.', 16, 1);

        begin tran;

        update Roles
        set IsDeleted = 1,
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