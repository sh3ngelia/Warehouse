using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class StorageDetailRepository : BaseRepository<StorageDetailDto>
{
    public StorageDetailRepository(DbConnection connection) : base(connection)
    {
    }
}