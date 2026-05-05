using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class RegionRepository : BaseRepository<RegionDto>
{
    public RegionRepository(DbConnection connection) : base(connection)
    {
    }
}
