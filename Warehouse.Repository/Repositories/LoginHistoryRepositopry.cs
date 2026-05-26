using System.Data.Common;
using Warehouse.DTO.Users;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class LoginHistoryRepositopry : BaseRepository<LoginHistoryDto>, ILoginHistoryRepository
{
    public LoginHistoryRepositopry(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}