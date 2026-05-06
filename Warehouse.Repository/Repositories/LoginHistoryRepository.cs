using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class LoginHistoryRepository : BaseRepository<LoginHistoryDto>, ILoginHistoryRepository
{
    public LoginHistoryRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}