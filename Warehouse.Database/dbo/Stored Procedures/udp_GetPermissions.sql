create procedure udp_GetPermissions
	@PermissionId int
as
begin
	set nocount on;
	select * from Permissions
	where PermissionId = @PermissionId
	and IsDeleted = 0;

end;