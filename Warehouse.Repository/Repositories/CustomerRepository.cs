using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class CustomerRepository : BaseRepository<CustomerDto>, ICustomerRepository
{
    public CustomerRepository(DbConnection connection) : base(connection)
    {
    }
}