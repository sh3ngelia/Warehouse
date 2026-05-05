using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("PhysicalCustomers")]
public sealed class PhysicalCustomerDto
{
    public int? CustomerId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PersonalId { get; set; } = default!;
}
