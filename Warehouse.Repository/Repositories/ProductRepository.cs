using System.Data.Common;
using Warehouse.DTO.Products;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class ProductRepository : BaseRepository<ProductDto>, IProductRepository
{
    public ProductRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}
