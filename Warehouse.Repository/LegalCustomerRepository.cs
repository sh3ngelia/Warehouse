using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class LegalCustomerRepository : BaseRepository<LegalCustomerDto>
{
    public LegalCustomerRepository(DbConnection connection) : base(connection)
    {
    }
}