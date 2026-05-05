using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Lookups;

[DbTable("ContractStatuses")]
public class ContractStatusDto
{
    public byte? ContractStatusId { get; set; }
    public string Name { get; set; } = null!;
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsDeleted { get; set; }
}