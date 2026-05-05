using System.Data.Common;
using Warehouse.DTO.Lookups;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class RoleRepository : BaseRepository<RoleDto>
{
    public RoleRepository(DbConnection connection) : base(connection)
    {
    }
}