using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Lookups;

[DbTable("Permissions")]
public class PermissionDto
{
    public int? PermissionId { get; set; }
    public string Name { get; set; } = null!;
    public short PermissionKey { get; set; }
    public string? Description { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsDeleted { get; set; }
}