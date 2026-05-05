using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public interface ICategoryRepository : IRepository<CategoryDto>
{
}

public sealed class CategoryRepository : BaseRepository<CategoryDto>, ICategoryRepository
{
    public CategoryRepository(DbConnection connection) : base(connection)
    {
    } 
}