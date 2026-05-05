using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class StorageRepository : BaseRepository<StorageDto>, IStorageRepository
{
    public StorageRepository(DbConnection connection) : base(connection)
    {
    }
}