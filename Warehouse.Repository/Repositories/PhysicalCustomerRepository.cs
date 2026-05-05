using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class PhysicalCustomerRepository : BaseRepository<PhysicalCustomerDto>, IPhysicalCustomerRepository
{
    public PhysicalCustomerRepository(DbConnection connection) : base(connection)
    {
    }
}