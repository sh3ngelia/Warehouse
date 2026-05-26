using System.Linq;
using Warehouse.DTO.Locations;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class CustomerRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithPhysicalCustomerType_ReturnsNewPositiveId()
    {
        // Arrange
        var dto = new CustomerDto
        {
            CustomerType = false,
            Phone = "5" + Guid.NewGuid().ToString("N")[..11],
            Email = $"p{Guid.NewGuid():N}"[..10] + "@t.com"
        };

        // Act
        var id = UnitOfWork.CustomerRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Insert_WithLegalCustomerType_ReturnsNewPositiveId()
    {
        // Arrange
        var dto = new CustomerDto
        {
            CustomerType = true,
            Phone = "5" + Guid.NewGuid().ToString("N")[..11],
            Email = $"l{Guid.NewGuid():N}"[..10] + "@t.com"
        };

        // Act
        var id = UnitOfWork.CustomerRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Get_WithExistingCustomerId_ReturnsMatchingCustomer()
    {
        // Arrange
        const int id = 1;
        const string expectedPhone = "599111111111";

        // Act
        var result = UnitOfWork.CustomerRepository.Get(id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Phone, Is.EqualTo(expectedPhone));
    }

    [Test]
    public void Get_WithNonExistingCustomerId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.CustomerRepository.Get(int.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByCustomerType_ReturnsOnlyMatchingCustomers()
    {
        // Act
        var result = UnitOfWork.CustomerRepository.Load(c => c.CustomerType == false).ToList();

        // Assert
        Assert.That(result, Is.Not.Empty);
        Assert.That(result.All(c => c.CustomerType == false), Is.True);
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        var result = UnitOfWork.CustomerRepository.Load(c => c.Phone == "__nonexistent__").ToList();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Update_WithChangedPhone_PersistsNewPhone()
    {
        // Arrange
        var dto = UnitOfWork.CustomerRepository.Get(2);
        Assert.That(dto, Is.Not.Null);

        var newPhone = "5" + Guid.NewGuid().ToString("N")[..11];
        dto!.Phone = newPhone;

        // Act
        UnitOfWork.CustomerRepository.Update(dto);

        // Assert
        var updated = UnitOfWork.CustomerRepository.Get(2);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.Phone, Is.EqualTo(newPhone));
    }

    [Test]
    public void Delete_WithExistingCustomerId_RemovesCustomerFromDatabase()
    {
        // Arrange
        const int id = 5;

        // Act
        UnitOfWork.CustomerRepository.Delete(id);

        // Assert
        var result = UnitOfWork.CustomerRepository.Get(id);
        Assert.That(result, Is.Null);
    }
}