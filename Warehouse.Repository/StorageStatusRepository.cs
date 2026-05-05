using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class StorageStatusRepository : BaseRepository<StorageStatusDto>
{
    public StorageStatusRepository(DbConnection connection) : base(connection)
    {
    }
}