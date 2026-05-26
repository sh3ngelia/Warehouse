using Dapper;
using System.Data;
using System.Data.Common;
using Warehouse.DTO.Customer;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class LegalCustomerRepository(DbConnection connection, Func<DbTransaction?> transaction) : BaseRepository<LegalCustomerDto>(connection, transaction), ILegalCustomerRepository
{
    private readonly DbConnection _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    private readonly Func<DbTransaction?> _transactionProvider = transaction ?? throw new ArgumentNullException(nameof(transaction));

    public override int Insert(LegalCustomerDto entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var parameters = new DynamicParameters();
        parameters.Add("@CustomerId", entity.CustomerId, DbType.Int32, ParameterDirection.InputOutput);
        parameters.Add("@Name", entity.Name, DbType.String);
        parameters.Add("@Address", entity.Address, DbType.String);

        _connection.Execute(
            "udp_InsertLegalCustomer",
            parameters,
            transaction: _transactionProvider(),
            commandType: CommandType.StoredProcedure);

        return parameters.Get<int>("@CustomerId");
    }

    public override void Update(LegalCustomerDto entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var parameters = new DynamicParameters();
        parameters.Add("@LegalCustomerId", entity.CustomerId, DbType.Int32);
        parameters.Add("@Name", entity.Name, DbType.String);
        parameters.Add("@Address", entity.Address, DbType.String);

        _connection.Execute(
            "udp_UpdateLegalCustomer",
            parameters,
            transaction: _transactionProvider(),
            commandType: CommandType.StoredProcedure
            );
    }
}