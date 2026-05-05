using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class ProductRepository : BaseRepository<ProductDto>
{
    public ProductRepository(DbConnection connection) : base(connection)
    {
    }
}
