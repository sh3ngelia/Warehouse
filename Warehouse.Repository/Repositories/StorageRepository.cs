using System.Data.Common;
using Warehouse.DTO.Storage;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class StorageRepository : BaseRepository<StorageDto>, IStorageRepository
{
    public StorageRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}