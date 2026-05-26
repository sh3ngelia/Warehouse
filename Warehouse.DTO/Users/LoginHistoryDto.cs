using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Users;

[DbTable("LoginHistory")]
public class LoginHistoryDto
{
    [IgnoreForInsert]
    public int? LoginHistoryId { get; set; }
    public int UserId { get; set; }
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public DateTime? LoginAt { get; set; }
    [IgnoreForInsert]
    public DateTime? LogoutAt { get; set; }
}
