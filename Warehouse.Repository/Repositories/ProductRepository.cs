using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class ProductRepository : BaseRepository<ProductDto>, IProductRepository
{
    public ProductRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}
