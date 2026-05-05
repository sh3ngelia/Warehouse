create procedure udp_DeletePermission
    @PermissionId   int
as
begin
    set nocount on;

    if not exists (select 1 from Permissions where PermissionId = @PermissionId and IsDeleted = 0)
    begin
        raiserror('Permission with the given ID does not exist.', 16, 1);
        return 1
    end

    update Permissions
    set IsDeleted = 1,
        UpdateDate = getdate()
    where PermissionId = @PermissionId;

    return 0;
end