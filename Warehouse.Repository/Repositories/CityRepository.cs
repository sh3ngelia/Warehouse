using System.Data.Common;
using Warehouse.DTO.Locations;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class CityRepository : BaseRepository<CityDto>, ICityRepository
{
    public CityRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}