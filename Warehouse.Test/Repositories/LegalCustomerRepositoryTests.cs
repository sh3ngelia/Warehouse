using System.Linq;
using Warehouse.DTO.Customer;
using Warehouse.DTO.Locations;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class LegalCustomerRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidLegalCustomer_ReturnsCustomerId()
    {
        // Arrange
        var customerId = UnitOfWork.CustomerRepository.Insert(new CustomerDto
        {
            CustomerType = true,
            Phone = "5" + Guid.NewGuid().ToString("N")[..11],
            Email = $"l{Guid.NewGuid():N}"[..10] + "@t.com"
        });

        var dto = new LegalCustomerDto
        {
            CustomerId = customerId,
            Name = "Legal_" + Guid.NewGuid().ToString("N")[..8],
            Address = "Address_" + Guid.NewGuid().ToString("N")[..8]
        };

        // Act
        var id = UnitOfWork.LegalCustomerRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Get_WithExistingCustomerId_ReturnsMatchingLegalCustomer()
    {
        // Arrange
        const int customerId = 3;
        const string expectedName = "TechCorp";

        // Act
        var result = UnitOfWork.LegalCustomerRepository.Get(customerId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Get_WithNonExistingCustomerId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.LegalCustomerRepository.Get(int.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByCompanyName_ReturnsOnlyMatchingLegalCustomers()
    {
        // Arrange
        const string companyName = "TechCorp";

        // Act
        var result = UnitOfWork.LegalCustomerRepository.Load(l => l.Name == companyName).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo(companyName));
    }

    [Test]
    public void Update_WithChangedAddress_PersistsNewAddress()
    {
        // Arrange
        var dto = UnitOfWork.LegalCustomerRepository.Get(3); // LogisticsPro
        Assert.That(dto, Is.Not.Null);

        dto!.Address = "Updated Legal Address";

        // Act
        UnitOfWork.LegalCustomerRepository.Update(dto);

        // Assert
        var updated = UnitOfWork.LegalCustomerRepository.Get(3);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.Address, Is.EqualTo("Updated Legal Address"));
    }

    [Test]
    public void Delete_WithExistingCustomerId_RemovesLegalCustomerFromDatabase()
    {
        // Arrange
        const int customerId = 4;

        // Act
        UnitOfWork.LegalCustomerRepository.Delete(customerId);

        // Assert
        var result = UnitOfWork.LegalCustomerRepository.Get(customerId);
        Assert.That(result, Is.Null);
    }
}