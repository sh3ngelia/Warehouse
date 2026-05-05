using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("Contracts")]
public class ContractDto
{
    public int? ContractId { get; set; }
    public int CustomerId { get; set; }
    public int EmployeeId { get; set; }
    public byte ContractStatus { get; set; }
    public DateTime? CreateDate { get; set; }
}
