using System.Data.Common;
using Warehouse.DTO.Lookups;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class PermissionRepository : BaseRepository<PermissionDto>, IPermissionRepository
{
    public PermissionRepository(DbConnection connection) : base(connection)
    {
    }
}