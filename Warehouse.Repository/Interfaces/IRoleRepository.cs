using Warehouse.DTO.Lookups;

namespace Warehouse.Repository.Interfaces;

public interface IRoleRepository : IBaseRepository<RoleDto>
{
    public int AssignPermissionToRole(int roleId, int permissionsId);
    public void UnassignPermissionFromRole(int roleId, int permissionsId);

}