CREATE PROCEDURE udp_UnassignRoleFromUser
    @UserId INT,
    @RoleId INT
AS
BEGIN
DELETE FROM UserRoles
WHERE UserId = @UserId AND RoleId = @RoleId
END