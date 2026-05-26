CREATE PROCEDURE udp_GetUserRoles
    @UserId INT
AS
BEGIN
    SELECT RoleId FROM UserRoles WHERE UserId = @UserId
END
