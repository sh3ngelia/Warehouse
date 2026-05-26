using System.Data.Common;
using Warehouse.DTO.Storage;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class StorageDetailRepository : BaseRepository<StorageDetailDto>, IStorageDetailRepository
{
    public StorageDetailRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}