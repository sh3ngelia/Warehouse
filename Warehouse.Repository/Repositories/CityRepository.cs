using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class CityRepository : BaseRepository<CityDto>, ICityRepository
{
    public CityRepository(DbConnection connection) : base(connection)
    {
    }
}