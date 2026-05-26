using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Users;

[DbTable("Users")]
public sealed class UserDto
{
    public int EmployeeId { get; set; }
    public string Username { get; set; } = default!;
    public byte[] Password { get; set; } = default!;
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public DateTime? CreateDate { get; set; }
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public DateTime? UpdateDate { get; set; }
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public bool? IsDeleted { get; set; }
}
