using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public interface IUserRepository : IRepository<UserDto>
{
    bool IsLoginUnique(string login);
}

public class UserRepository : BaseRepository<UserDto>, IUserRepository
{
    public UserRepository(DbConnection connection) : base(connection)
    {
    }

    public bool IsLoginUnique(string login)
    {
        throw new NotImplementedException();
    }
}