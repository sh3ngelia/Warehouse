using Warehouse.Extension.Attributes;

namespace Warehouse.DTO.Main;

[DbTable("Products")]
public sealed class ProductDto
{
    public int? ProductId { get; set; }
    public int CategoryId { get; set; }
    public string Name { get; set; } = default!;
    public string SKU { get; set; } = default!;
    public string? Description { get; set; }
    public DateTime? CreateDate { get; set; }
    public DateTime? UpdateDate { get; set; }
    public bool? IsDeleted { get; set; }
}
