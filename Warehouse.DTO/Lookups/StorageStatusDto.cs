using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Lookups;

[DbTable("StorageStatuses")]
public class StorageStatusDto
{
    [IgnoreForInsert]
    public int? StorageStatusId { get; set; }
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