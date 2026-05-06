using System.Data.Common;
using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class LegalCustomerRepository : BaseRepository<LegalCustomerDto>, ILegalCustomerRepository
{
    public LegalCustomerRepository(DbConnection connection, Func<DbTransaction?> transaction) : base(connection, transaction)
    {
    }
}