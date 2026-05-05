using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class StorageDetailRepository : BaseRepository<StorageDetailDto>, IStorageDetailRepository
{
    public StorageDetailRepository(DbConnection connection) : base(connection)
    {
    }
}