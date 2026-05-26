using Warehouse.DTO.Lookups;

namespace Warehouse.Service.Abstracts;

public interface IRoleService : ICrudService<RoleDto>
{
    void AssignRoleToUser(int userId, int roleId);
    void UnassignRoleFromUser(int userId, int roleId);
    IEnumerable<int> GetRolesForUser(int userId);
}