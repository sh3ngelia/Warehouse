using System.Data.Common;
using Warehouse.DTO.Products;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal sealed class CategoryRepository : BaseRepository<CategoryDto>, ICategoryRepository
{
    public CategoryRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    } 
}