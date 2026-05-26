using Dapper;
using System.Data;
using System.Data.Common;
using Warehouse.DTO.Customer;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class PhysicalCustomerRepository(DbConnection connection, Func<DbTransaction?> transaction) : BaseRepository<PhysicalCustomerDto>(connection, transaction), IPhysicalCustomerRepository
{
    private readonly DbConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    private readonly Func<DbTransaction?> _transactionProvider = transaction ?? throw new ArgumentNullException(nameof(transaction));

    public override int Insert(PhysicalCustomerDto entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var parameters = new DynamicParameters();
        parameters.Add("@CustomerId", entity.CustomerId, DbType.Int32, ParameterDirection.InputOutput);
        parameters.Add("@FirstName", entity.FirstName, DbType.String);
        parameters.Add("@LastName", entity.LastName, DbType.String);
        parameters.Add("@PersonalId", entity.PersonalId, DbType.String);

        _connection.Execute(
            "udp_InsertPhysicalCustomer",
            parameters,
            transaction: _transactionProvider(),
            commandType: CommandType.StoredProcedure);

        return parameters.Get<int>("@CustomerId");
    }

    public override void Update(PhysicalCustomerDto entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var parameters = new DynamicParameters();
        parameters.Add("@CustomerId", entity.CustomerId, DbType.Int32);
        parameters.Add("@FirstName", entity.FirstName, DbType.String);
        parameters.Add("@LastName", entity.LastName, DbType.String);
        parameters.Add("@PersonalId", entity.PersonalId, DbType.String);

        _connection.Execute(
            "udp_UpdatePhysicalCustomer",
            parameters,
            transaction: _transactionProvider(),
            commandType: CommandType.StoredProcedure
        );
    }
}