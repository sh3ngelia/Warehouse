using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("Employees")]
public class EmployeeDto
{
    [IgnoreForInsert]
    public int? EmployeeId { get; set; }
    public required string PersonalId { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
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
