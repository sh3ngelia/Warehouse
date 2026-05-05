using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public sealed class CategoryRepository : BaseRepository<CategoryDto>, ICategoryRepository
{
    public CategoryRepository(DbConnection connection) : base(connection)
    {
    } 
}