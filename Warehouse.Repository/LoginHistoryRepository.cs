using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class LoginHistoryRepositopry : BaseRepository<LoginHistoryDto>
{
    public LoginHistoryRepositopry(DbConnection connection) : base(connection)
    {
    }
}