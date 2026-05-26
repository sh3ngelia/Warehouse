using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Products;

[DbTable("Products")]
public sealed class ProductDto
{
    [IgnoreForInsert]
    public int? ProductId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public string SKU { get; set; } = default!;
    public string? Description { get; set; }
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
