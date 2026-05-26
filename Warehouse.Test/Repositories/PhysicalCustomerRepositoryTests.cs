using System.Linq;
using Warehouse.DTO.Customer;
using Warehouse.DTO.Locations;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class PhysicalCustomerRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidPhysicalCustomer_ReturnsCustomerId()
    {
        // Arrange
        var customerId = UnitOfWork.CustomerRepository.Insert(new CustomerDto
        {
            CustomerType = false,
            Phone = "5" + Guid.NewGuid().ToString("N")[..11],
            Email = $"p{Guid.NewGuid():N}"[..10] + "@t.com"
        });

        var dto = new PhysicalCustomerDto
        {
            CustomerId = customerId,
            FirstName = "Test",
            LastName = "Physical",
            PersonalId = Guid.NewGuid().ToString("N")[..11]
        };

        // Act
        var id = UnitOfWork.PhysicalCustomerRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Get_WithExistingCustomerId_ReturnsMatchingPhysicalCustomer()
    {
        // Arrange
        const int customerId = 1;
        const string expectedPersonalId = "11111111111";

        // Act
        var result = UnitOfWork.PhysicalCustomerRepository.Get(customerId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.PersonalId, Is.EqualTo(expectedPersonalId));
    }

    [Test]
    public void Get_WithNonExistingCustomerId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.PhysicalCustomerRepository.Get(int.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByLastName_ReturnsOnlyMatchingPhysicalCustomers()
    {
        // Arrange
        const string lastName = "Chikovani";

        // Act
        var result = UnitOfWork.PhysicalCustomerRepository.Load(p => p.LastName == lastName).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().LastName, Is.EqualTo(lastName));
    }

    [Test]
    public void Update_WithChangedLastName_PersistsNewLastName()
    {
        // Arrange
        var dto = UnitOfWork.PhysicalCustomerRepository.Get(2);
        Assert.That(dto, Is.Not.Null);

        dto!.LastName = "UpdatedLastName";

        // Act
        UnitOfWork.PhysicalCustomerRepository.Update(dto);

        // Assert
        var updated = UnitOfWork.PhysicalCustomerRepository.Get(2);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.LastName, Is.EqualTo("UpdatedLastName"));
    }

    [Test]
    public void Delete_WithExistingCustomerId_RemovesPhysicalCustomerFromDatabase()
    {
        // Arrange
        const int customerId = 5;

        // Act
        UnitOfWork.PhysicalCustomerRepository.Delete(customerId);

        // Assert
        var result = UnitOfWork.PhysicalCustomerRepository.Get(customerId);
        Assert.That(result, Is.Null);
    }
}