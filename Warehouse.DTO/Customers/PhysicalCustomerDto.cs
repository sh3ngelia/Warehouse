using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Customer;

[DbTable("PhysicalCustomers")]
public sealed class PhysicalCustomerDto
{
    [IgnoreForInsert]
    public int? CustomerId { get; set; }
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public string PersonalId { get; set; } = default!;
}
