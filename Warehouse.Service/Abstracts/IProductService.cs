using Warehouse.DTO.Products;
using Warehouse.DTO.Storage;

namespace Warehouse.Service.Abstracts;

public interface IProductService
{
    int InsertCategory(CategoryDto dto);
    void UpdateCategory(CategoryDto dto);
    void DeleteCategory(int categoryId);
    int Insert(ProductDto product);
    void Update(ProductDto product);
    void Delete(int productId);
    void AssignProductToCategory(int productId, int categoryId);
    int GetTotalStock(int productId);
    IEnumerable<StorageDto> GetStorageLocations(int productId);
    IEnumerable<ProductDto> GetByCategory(int categoryId);
    IEnumerable<ProductDto> GetInStock();
    IEnumerable<ProductDto> GetOutOfStock();
}