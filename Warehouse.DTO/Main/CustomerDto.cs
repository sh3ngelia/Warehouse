using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("Customers")]
public class CustomerDto
{
    public int? CustomerId { get; set; }
    public bool CustomerType { get; set; }
    public required string Phone { get; set; }
    public string? Email { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsDeleted { get; set; }
}
