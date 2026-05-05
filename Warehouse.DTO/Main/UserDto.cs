using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("Users")]
public sealed class UserDto
{
    public int? EmployeeId { get; set; }
    public string Username { get; set; } = default!;
    public byte[] Password { get; set; } = default!;
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsDeleted { get; set; }
}
