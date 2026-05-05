using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("Regions")]
public class RegionDto
{
    public int? RegionId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsDeleted { get; set; }
}