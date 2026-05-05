using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("Cities")]
public class CityDto
{
    public int? CityId { get; set; }
    public int RegionId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsDeleted { get; set; }
}

