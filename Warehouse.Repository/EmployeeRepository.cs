using System.Data.Common;
using Warehouse.DTO.Main;

namespace Warehouse.Repository;

public class EmployeeRepository : BaseRepository<EmployeeDto>
{
    public EmployeeRepository(DbConnection connection) : base(connection)
    {
    }
}