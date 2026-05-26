using Dapper;
using System.Data;
using System.Data.Common;
using Warehouse.DTO.Users;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class EmployeeRepository(DbConnection connection, Func<DbTransaction?> transaction) : BaseRepository<EmployeeDto>(connection, transaction), IEmployeeRepository
{
    private readonly DbConnection? _connection = connection ?? throw new ArgumentNullException(nameof(connection));
    private readonly Func<DbTransaction?> _transactionProvider = transaction ?? throw new ArgumentNullException(nameof(transaction));

    public override int Insert(EmployeeDto entity)
    {
        ArgumentNullException.ThrowIfNull(entity);

        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", entity.EmployeeId, DbType.Int32, direction: ParameterDirection.InputOutput);
        parameters.Add("@PersonalId", entity.PersonalId, DbType.String);
        parameters.Add("@FirstName", entity.FirstName, DbType.String);
        parameters.Add("@LastName", entity.LastName, DbType.String);
        parameters.Add("@Phone", entity.Phone, DbType.String);
        parameters.Add("@Email", entity.Email, DbType.String);

        _connection!.Execute(
            "udp_InsertEmployee",
            parameters,
            transaction: _transactionProvider(),
            commandType: CommandType.StoredProcedure);

        return parameters.Get<int>("@EmployeeId");
    }
}