using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("Storages")]
public sealed class StorageDto
{
    public int? StorageId { get; set; }
    public int Status { get; set; }
    public int CityId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public double Capacity { get; set; }
    public string Address { get; set; } = default!;
    public decimal Price { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsDeleted { get; set; }
}
