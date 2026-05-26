using System.Data;
using Microsoft.Data.SqlClient;
using Warehouse.Test.Configuration;

namespace Warehouse.Test.Repositories.DbTools;

internal static class DbHelper
{
    public static void SeedTestData() => ExecuteStoredProcedure("udp_SeedTestData");

    public static void ClearTestData() => ExecuteStoredProcedure("udp_ClearTestData");

    private static void ExecuteStoredProcedure(string procedureName)
    {
        using var connection = new SqlConnection(ConfigurationManager.ConnectionString);
        using var command = connection.CreateCommand();
        command.CommandText = procedureName;
        command.CommandType = CommandType.StoredProcedure;
        connection.Open();
        command.ExecuteNonQuery();
    }
}