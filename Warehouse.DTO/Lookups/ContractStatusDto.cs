using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Lookups;

[DbTable("ContractStatuses")]
public class ContractStatusDto
{
    [IgnoreForInsert]
    public byte? ContractStatusId { get; set; }
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