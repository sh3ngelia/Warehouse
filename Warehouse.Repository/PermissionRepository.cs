using System.Data.Common;
using Warehouse.DTO.Lookups;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class PermissionRepository : BaseRepository<PermissionDto>
{
    public PermissionRepository(DbConnection connection) : base(connection)
    {
    }
}