using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class LoginHistoryRepositopry : BaseRepository<LoginHistoryDto>, ILoginHistoryRepository
{
    public LoginHistoryRepositopry(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}