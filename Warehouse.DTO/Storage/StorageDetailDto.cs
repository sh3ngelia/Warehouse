using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Storage;

[DbTable("StorageDetails")]
public sealed class StorageDetailDto
{
    [IgnoreForInsert]
    public int? StorageDetailId { get; set; }
    public int ContractDetailId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}
