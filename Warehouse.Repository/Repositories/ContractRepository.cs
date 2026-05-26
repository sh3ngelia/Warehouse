using System.Data.Common;
using Warehouse.DTO.Contracts;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class ContractRepository : BaseRepository<ContractDto>, IContractRepository
{
    public ContractRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}