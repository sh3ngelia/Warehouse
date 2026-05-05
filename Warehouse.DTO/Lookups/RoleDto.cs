using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Lookups;

[DbTable("Roles")]
public class RoleDto
{
    public int? RoleId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsDeleted { get; set; }
}