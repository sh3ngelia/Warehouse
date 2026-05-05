create procedure udp_AssignPermissionToRole
    @RoleId         int,
    @PermissionId   int
as
begin
    set nocount on;

    insert into RolePermissions(RoleId, PermissionId)
    values(@RoleId, @PermissionId);

    return 0;
end