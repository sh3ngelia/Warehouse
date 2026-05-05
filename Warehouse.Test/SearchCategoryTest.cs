using Microsoft.Data.SqlClient;
using System.Data;
using Warehouse.DTO.Main;

namespace Warehouse.Test
{
    public class Tests
    {
        private SqlConnection? _connection;

        private readonly string _connectionString =
            "server=BESODE-NOTE; database=Warehouse.Database; Integrated Security=true; TrustServerCertificate=true";

        [SetUp]
        public void Setup()
        {
            _connection = new SqlConnection(_connectionString);
            _connection.Open();
        }

        [TearDown]
        public void TearDown()
        {
            _connection?.Dispose();
        }

        /*[Test]
        public void InsertRecordOfProducts()
        {
            if (_connection != null)
            {
                // Arrange
                var repository = new Warehouse.Repository.ProductRepository(_connection);
                var product = new ProductDto
                {
                    CategoryId = 2,
                    Name = "Test2",
                    SKU = "98765298wqeqw470871623",
                    Description = "Tes123123t description"
                };
                // Act
                var id = repository.Insert(product);
                // Assert
                Assert.IsTrue(id > 0);
            }
        }*/

        [Test]
        public void InsertRecordOfRegions()
        {
            if (_connection != null)
            {
                // Arrange
                var repository = new Warehouse.Repository.RegionRepository(_connection);
                var region = new RegionDto
                {
                    Name = "TestRegion"
                };
                // Act
                var id = repository.Insert(region);
                // Assert
                Assert.IsTrue(id > 0);
            }
        }

        [Test]
        public void LoadCategoriesByNameAndIsActive()
        {
            if (_connection != null)
            {
                // Arrange
                var repository = new Warehouse.Repository.CategoryRepository(_connection);
                // Act
                var categories = repository.Load(c => c.CategoryName == "Test1" || c.CreateDate > new DateTime(2026, 02, 13, 11, 12, 48));
                // Assert
                Assert.That(categories.Count(), Is.EqualTo(5));
            }
        }
    }
}