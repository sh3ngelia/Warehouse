using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Locations;

[DbTable("Regions")]
public class RegionDto
{
    [IgnoreForInsert]
    public int? RegionId { get; set; }
    public string Name { get; set; } = null!;
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