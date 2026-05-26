using Warehouse.DTO.Users;

namespace Warehouse.Service.Abstracts
{
    public interface IAuthorizationService
    {
        UserDto? Login(string username, string password);
        public bool Logout(int userId);
    }
}
