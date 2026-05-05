using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

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