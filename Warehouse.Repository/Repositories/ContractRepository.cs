using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class ContractRepository : BaseRepository<ContractDto>, IContractRepository
{
    public ContractRepository(DbConnection connection) : base(connection)
    {
    }
}