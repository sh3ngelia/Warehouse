using System.Data.Common;
using Warehouse.DTO.Lookups;

namespace Warehouse.Repository;

public class ContractStatusRepository : BaseRepository<ContractStatusDto>
{
    public ContractStatusRepository(DbConnection connection) : base(connection)
    {
    }
}