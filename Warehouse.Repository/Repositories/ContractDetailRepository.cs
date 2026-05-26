using System.Data.Common;
using Warehouse.DTO.Contracts;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class ContractDetailRepository : BaseRepository<ContractDetailDto>, IContractDetailRepository
{
    public ContractDetailRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}