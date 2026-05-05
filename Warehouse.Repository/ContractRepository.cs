using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class ContractRepository : BaseRepository<ContractDto>
{
    public ContractRepository(DbConnection connection) : base(connection)
    {
    }
}