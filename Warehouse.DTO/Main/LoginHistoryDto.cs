using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("LoginHistory")]
public class LoginHistoryDto
{
    [IgnoreForInsert]
    public int? LoginHistoryId { get; set; }
    public int UserId { get; set; }
    public DateTime? LoginAt { get; set; }
}
