using Microsoft.Data.SqlClient;
using Warehouse.Repository.UnitOfWork;

namespace Warehouse.Test
{
    public abstract class RepositoryTestBase
    {
        private readonly string _connectionString = "server=BESODE-NOTE; database=Warehouse.Database; Integrated Security=true; TrustServerCertificate=true";
        private SqlConnection _connection = null!;
        private UnitOfWork _unitOfWork = null!; 

        protected UnitOfWork UnitOfWork => _unitOfWork; // since RepositoryTestBase children will use it, we need to make it protected

        [SetUp]
        public void BaseSetup()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();

            _unitOfWork = new UnitOfWork(_connection);
            _unitOfWork.BeginTransaction();
        }

        [TearDown]
        public void BaseTearDown()
        {
            _unitOfWork.RollbackTransaction();
            _unitOfWork.Dispose();
            _connection.Dispose();
        }


    }
}