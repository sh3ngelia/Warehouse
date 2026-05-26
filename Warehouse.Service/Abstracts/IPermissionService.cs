using Warehouse.DTO.Lookups;

namespace Warehouse.Service.Abstracts
{
    public interface IPermissionService : ICrudService<PermissionDto>
    {
        public void UnassignPermissionFromRole(int roleId, int permissionId);
        public void AssignPermissionToRole(int roleId, int permissionId);
    }
}
