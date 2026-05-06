using System.Data.Common;
using Warehouse.DTO.Lookups;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class ContractStatusRepository : BaseRepository<ContractStatusDto>, IContractStatusRepository
{
    public ContractStatusRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}