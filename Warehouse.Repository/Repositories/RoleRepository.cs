using Dapper;
using System.Data.Common;
using Warehouse.DTO.Lookups;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.Repositories;

internal class RoleRepository(DbConnection connection, Func<DbTransaction?> transaction) : 
    BaseRepository<RoleDto>(connection, transaction), IRoleRepository
    {
        public int AssignPermissionToRole(int roleId, int permissionsId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleId", roleId);
            parameters.Add("@PermissionId", permissionsId);

            connection.Execute(
                "udp_AssignPermissionToRole",
                parameters,
                transaction: transaction(),
                commandType: System.Data.CommandType.StoredProcedure);

            return parameters.Get<int>("@PermissionId");
        }

        public void UnassignPermissionFromRole(int roleId, int permissionsId)
        {
            var parameters = new DynamicParameters();
            parameters.Add("@RoleId", roleId);
            parameters.Add("@PermissionId", permissionsId);
            connection.Execute(
                "udp_UnassignPermissionFromRole",
                parameters,
                transaction: transaction(),
                commandType: System.Data.CommandType.StoredProcedure);
        }
    }
