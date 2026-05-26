using System.Security;
using Warehouse.DTO.Lookups;
using Warehouse.Service.Abstracts;

using Warehouse.Repository.UnitOfWork;

namespace Warehouse.Service.Users
{
    internal class PermissionService : CrudService<PermissionDto>, IPermissionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public PermissionService(IUnitOfWork unitOfWork) : base(unitOfWork.PermissionRepository)
        {
            _unitOfWork = unitOfWork;
        }
        public void UnassignPermissionFromRole(int roleId, int permissionId)
        {
            ArgumentNullException.ThrowIfNull(roleId);
            ArgumentNullException.ThrowIfNull(permissionId);

            _unitOfWork.RoleRepository.UnassignPermissionFromRole(roleId, permissionId);
        }

        public void AssignPermissionToRole(int roleId, int permissionId)
        {
            ArgumentNullException.ThrowIfNull(roleId);
            ArgumentNullException.ThrowIfNull(permissionId);

            _unitOfWork.RoleRepository.AssignPermissionToRole(roleId, permissionId);
        }
    }
}
