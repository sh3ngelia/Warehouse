using Dapper;
using System.Data;
using System.Data.Common;
using Warehouse.DTO.Users;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class UserRepository(DbConnection connection, Func<DbTransaction?> transaction) : BaseRepository<UserDto>(connection, transaction), IUserRepository
{
    private readonly DbConnection _connection = connection;
    private readonly Func<DbTransaction?> _transaction = transaction;

    public override int Insert(UserDto entity)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@EmployeeId", entity.EmployeeId, dbType: DbType.Int32, direction: ParameterDirection.InputOutput);
        parameters.Add("@Username", entity.Username, dbType: DbType.String);
        parameters.Add("@Password", entity.Password, dbType: DbType.Binary);

        _connection!.Execute(
            "udp_InsertUser",
            parameters,
            transaction: _transaction(),
            commandType: CommandType.StoredProcedure);

        return parameters.Get<int>("@EmployeeId");
    }

    public UserDto? Login(string username, string password) 
    {
        var parameters = new DynamicParameters();
        parameters.Add("@Username", username);
        parameters.Add("@Password", password);

        return _connection.QueryFirstOrDefault<UserDto>(
            "udp_UserLogin",
            parameters,
            transaction: _transaction(),
            commandType: CommandType.StoredProcedure);
    }

    public int AssignRoleToUser(int userId, int roleId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);
        parameters.Add("@RoleId", roleId);

        _connection.Execute(
            "udp_AssignRoleToUser",
            parameters,
            transaction: _transaction(),
            commandType: CommandType.StoredProcedure);

        return parameters.Get<int>("@RoleId");
    }

    public void UnassignRoleFromUser(int userId, int roleId)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@UserId", userId);
        parameters.Add("@RoleId", roleId);

        _connection.Execute(
            "udp_UnassignRoleFromUser",
            parameters,
            transaction: _transaction(),
            commandType: CommandType.StoredProcedure);
    }

    public IEnumerable<int> GetRolesForUser(int userId)
    {
        return _connection.Query<int>(
            "udp_GetUserRoles",
            new { UserId = userId },
            transaction: _transaction(),
            commandType: CommandType.StoredProcedure);
    }
}