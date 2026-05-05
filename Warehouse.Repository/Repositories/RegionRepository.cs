using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class RegionRepository : BaseRepository<RegionDto>, IRegionRepository
{
    public RegionRepository(DbConnection connection) : base(connection)
    {
    }
}
