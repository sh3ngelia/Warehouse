using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class StorageStatusRepository : BaseRepository<StorageStatusDto>, IStorageStatusRepository
{
    public StorageStatusRepository(DbConnection connection) : base(connection)
    {
    }
}