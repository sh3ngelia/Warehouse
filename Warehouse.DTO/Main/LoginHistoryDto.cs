using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("LoginHistories")]
public class LoginHistoryDto
{
    public int? LoginHistoryId { get; set; }
    public int UserId { get; set; }
    public DateTime? LoginAt { get; set; }
}
