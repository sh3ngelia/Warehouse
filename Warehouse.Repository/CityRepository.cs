using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class CityRepository : BaseRepository<CityDto>
{
    public CityRepository(DbConnection connection) : base(connection)
    {
    }
}