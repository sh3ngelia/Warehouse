using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("StorageStatuses")]
public sealed class StorageStatusDto
{
    public int? StorageStatusId { get; set; }
    public string Name { get; set; } = default!;
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsDeleted { get; set; }
}
