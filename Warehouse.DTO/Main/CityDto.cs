using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("Cities")]
public class CityDto
{
    [IgnoreForInsert]
    public int? CityId { get; set; }
    public int RegionId { get; set; }
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

