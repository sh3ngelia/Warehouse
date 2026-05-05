create procedure udp_GetRoles
    @RoleId int
as
begin
    set nocount on;

    select * from Roles
    where RoleId = @RoleId
       and IsDeleted = 0;

end

