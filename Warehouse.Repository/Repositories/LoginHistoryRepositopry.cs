using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class LoginHistoryRepositopry : BaseRepository<LoginHistoryDto>, ILoginHistoryRepository
{
    public LoginHistoryRepositopry(DbConnection connection) : base(connection)
    {
    }
}