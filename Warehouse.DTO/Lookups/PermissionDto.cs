using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Lookups;

[DbTable("Permissions")]
public class PermissionDto
{
    [IgnoreForInsert]
    public int? PermissionId { get; set; }
    public string Name { get; set; } = null!;
    public short PermissionKey { get; set; }
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public string? Description { get; set; }
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