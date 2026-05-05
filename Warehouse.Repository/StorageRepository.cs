using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class StorageRepository : BaseRepository<StorageDto>
{
    public StorageRepository(DbConnection connection) : base(connection)
    {
    }
}