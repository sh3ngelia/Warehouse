using Warehouse.DTO.Lookups;
using Warehouse.Repository.UnitOfWork;
using Warehouse.Service.Abstracts;

namespace Warehouse.Service.Users;

internal class RoleService : CrudService<RoleDto>, IRoleService
{
    private readonly IUnitOfWork _unitOfWork;

    public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork.RoleRepository)
    {
        _unitOfWork = unitOfWork;
    }

    public void AssignRoleToUser(int userId, int roleId)
    {
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(roleId);

        _unitOfWork.UserRepository.AssignRoleToUser(userId, roleId);
    }

    public void UnassignRoleFromUser(int userId, int roleId)
    {
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(roleId);

        _unitOfWork.UserRepository.UnassignRoleFromUser(userId, roleId);
    }
    public IEnumerable<int> GetRolesForUser(int userId)
    {
        return _unitOfWork.UserRepository.GetRolesForUser(userId);
    }
}