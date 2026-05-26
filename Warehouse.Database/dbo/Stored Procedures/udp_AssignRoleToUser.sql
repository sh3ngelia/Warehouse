CREATE PROCEDURE udp_AssignRoleToUser
    @UserId INT,
    @RoleId INT
AS
BEGIN
    IF NOT EXISTS (SELECT 1 FROM UserRoles WHERE UserId = @UserId AND RoleId = @RoleId)
        INSERT INTO UserRoles (UserId, RoleId) VALUES (@UserId, @RoleId)
END