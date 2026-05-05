using System.Data.Common;
using Warehouse.DTO.Lookups;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class RoleRepository : BaseRepository<RoleDto>, IRoleRepository
{
    public RoleRepository(DbConnection connection) : base(connection)
    {
    }
}