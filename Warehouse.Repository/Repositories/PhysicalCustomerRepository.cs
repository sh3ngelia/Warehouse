using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class PhysicalCustomerRepository : BaseRepository<PhysicalCustomerDto>, IPhysicalCustomerRepository
{
    public PhysicalCustomerRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}