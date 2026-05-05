using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("Categories")]
public sealed class CategoryDto
{
    [IgnoreForInsert]
    public int? CategoryId { get; set; }
    public string CategoryName { get; set; } = null!;

    [IgnoreForInsert]
    [IgnoreForUpdate]
    public DateTime? CreateDate { get; set; } // nullable

    [IgnoreForInsert]
    [IgnoreForUpdate]
    public DateTime? UpdateDate { get; set; }

    [IgnoreForInsert]
    [IgnoreForUpdate]
    public bool? IsDeleted { get; set; }
}