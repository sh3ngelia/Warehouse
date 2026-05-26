using Warehouse.DTO.Users;

namespace Warehouse.Repository.Interfaces;

public interface IUserRepository : IBaseRepository<UserDto>
{
    UserDto? Login(string username, string password);
    public int AssignRoleToUser(int userId, int roleId);
    public void UnassignRoleFromUser(int userId, int roleId);
    IEnumerable<int> GetRolesForUser(int userId);
}