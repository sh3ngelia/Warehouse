using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

public class LegalCustomerRepository : BaseRepository<LegalCustomerDto>, ILegalCustomerRepository
{
    public LegalCustomerRepository(DbConnection connection) : base(connection)
    {
    }
}