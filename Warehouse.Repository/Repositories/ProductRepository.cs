using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class ProductRepository : BaseRepository<ProductDto>, IProductRepository
{
    public ProductRepository(DbConnection connection) : base(connection)
    {
    }
}
