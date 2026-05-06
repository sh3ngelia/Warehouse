using System.Data.Common;
using Warehouse.DTO.Lookups;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class StorageStatusRepository : BaseRepository<StorageStatusDto>, IStorageStatusRepository
{
    public StorageStatusRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}