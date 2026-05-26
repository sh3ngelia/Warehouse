using System.Data.Common;
using Warehouse.DTO.Users;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class LoginHistoryRepository : BaseRepository<LoginHistoryDto>, ILoginHistoryRepository
{
    public LoginHistoryRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}