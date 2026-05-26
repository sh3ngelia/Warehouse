using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Storage;

[DbTable("Storages")]
public sealed class StorageDto
{
    [IgnoreForInsert]
    public int? StorageId { get; set; }
    public int Status { get; set; }
    public int CityId { get; set; }
    public string Name { get; set; } = default!;
    public string? Description { get; set; }
    public double Capacity { get; set; }
    public string Address { get; set; } = default!;
    public decimal Price { get; set; }
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
