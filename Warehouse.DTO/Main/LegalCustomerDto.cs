using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("LegalCustomers")]
public class LegalCustomerDto
{
    [IgnoreForInsert]
    public int? CustomerId { get; set; }
    public required string Name { get; set; }
    public required string Address { get; set; }  
}
