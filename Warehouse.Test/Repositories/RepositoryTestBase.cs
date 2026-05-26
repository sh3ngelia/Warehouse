using Microsoft.Data.SqlClient;
using Warehouse.Repository.UnitOfWork;
using Warehouse.Test.Configuration;
using Warehouse.Test.Repositories.DbTools;

namespace Warehouse.Test.Repositories
{
    public abstract class RepositoryTestBase
    {
        private SqlConnection _connection = null!;
        protected UnitOfWork UnitOfWork = null!; 

        [SetUp]
        public void SetUp()
        {
            _connection = new SqlConnection(ConfigurationManager.ConnectionString);
            UnitOfWork = new UnitOfWork(_connection);
            _connection.Open();
        }

        [TearDown]
        public void TearDown()
        {
            UnitOfWork.Dispose();
            _connection.Dispose();
        }

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            DbHelper.ClearTestData();
            DbHelper.SeedTestData();
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            DbHelper.ClearTestData();
        }
    }
}