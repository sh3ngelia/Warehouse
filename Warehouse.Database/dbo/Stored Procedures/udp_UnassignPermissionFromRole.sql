create procedure udp_UnassignPermissionFromRole
    @RoleId         int,
    @PermissionId   int
as
begin
    set nocount on;

    delete from RolePermissions
    where RoleId = @RoleId and PermissionId = @PermissionId;
    
    return 0;
end