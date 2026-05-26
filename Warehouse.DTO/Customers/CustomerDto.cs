using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Locations;

[DbTable("Customers")]
public class CustomerDto
{
    [IgnoreForInsert]
    public int? CustomerId { get; set; }
    public bool CustomerType { get; set; }
    public required string Phone { get; set; }
    public required string Email { get; set; }
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
