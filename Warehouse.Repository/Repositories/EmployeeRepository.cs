using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class EmployeeRepository : BaseRepository<EmployeeDto>, IEmployeeRepository
{
    public EmployeeRepository(DbConnection connection) : base(connection)
    {
    }
}