using System.Data.Common;
using Warehouse.DTO.Locations;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class CustomerRepository : BaseRepository<CustomerDto>, ICustomerRepository
{
    public CustomerRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}