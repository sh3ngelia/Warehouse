using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class CustomerRepository : BaseRepository<CustomerDto>
{
    public CustomerRepository(DbConnection connection) : base(connection)
    {
    }
}