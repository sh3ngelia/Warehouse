using System.Data.Common;
using Warehouse.DTO.Lookups;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class ContractStatusRepository : BaseRepository<ContractStatusDto>, IContractStatusRepository
{
    public ContractStatusRepository(DbConnection connection) : base(connection)
    {
    }
}