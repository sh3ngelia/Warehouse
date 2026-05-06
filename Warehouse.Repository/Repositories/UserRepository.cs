using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class UserRepository : BaseRepository<UserDto>, IUserRepository
{
    public UserRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }

    public bool IsLoginUnique(string login)
    {
        throw new NotImplementedException();
    }
}