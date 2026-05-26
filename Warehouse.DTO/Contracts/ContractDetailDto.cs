using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Contracts;

[DbTable("ContractDetails")]
public class ContractDetailDto
{
    [IgnoreForInsert]
    public int ContractDetailId { get; set; }
    public int ContractId { get; set; }
    public int StorageId { get; set; }
    public decimal Price { get; set; }
}