using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class ContractDetailRepository : BaseRepository<ContractDetailDto>
{
    public ContractDetailRepository(DbConnection connection) : base(connection)
    {
    }
}