using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class PhysicalCustomerRepository : BaseRepository<PhysicalCustomerDto>
{
    public PhysicalCustomerRepository(DbConnection connection) : base(connection)
    {
    }
}