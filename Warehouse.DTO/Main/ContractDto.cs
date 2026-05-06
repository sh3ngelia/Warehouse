using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("Contracts")]
public class ContractDto
{
    [IgnoreForInsert]
    public int? ContractId { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public byte ContractStatus { get; set; }
    [IgnoreForInsert]
    [IgnoreForUpdate]
    public DateTime? CreateDate { get; set; }
}
