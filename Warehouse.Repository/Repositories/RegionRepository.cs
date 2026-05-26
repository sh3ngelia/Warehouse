using System.Data.Common;
using Warehouse.DTO.Locations;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class RegionRepository : BaseRepository<RegionDto>, IRegionRepository
{
    public RegionRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}
