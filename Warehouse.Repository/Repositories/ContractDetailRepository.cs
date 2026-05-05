using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class ContractDetailRepository : BaseRepository<ContractDetailDto>, IContractDetailRepository
{
    public ContractDetailRepository(DbConnection connection) : base(connection)
    {
    }
}