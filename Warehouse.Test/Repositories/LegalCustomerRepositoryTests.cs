using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class LegalCustomerRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidLegalCustomer_ReturnsCustomerId()
    {
        // Arrange
        // TODO: insert a CustomerDto with CustomerType = true (legal) to get a valid CustomerId
        // TODO: create a LegalCustomerDto using that CustomerId, with Name and Address

        // Act
        // TODO: call UnitOfWork.LegalCustomerRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingCustomerId_ReturnsMatchingLegalCustomer()
    {
        // Arrange
        // TODO: insert Customer + LegalCustomer, capture customerId

        // Act
        // TODO: call UnitOfWork.LegalCustomerRepository.Get(customerId)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.Name, Is.EqualTo(expected name))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingCustomerId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.LegalCustomerRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByCompanyName_ReturnsOnlyMatchingLegalCustomers()
    {
        // Arrange
        // TODO: insert two Customer + LegalCustomer pairs with distinct company names

        // Act
        // TODO: call Load(l => l.Name == insertedName)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(1))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedAddress_PersistsNewAddress()
    {
        // Arrange
        // TODO: insert Customer + LegalCustomer, Get(customerId) to retrieve entity

        // Act
        // TODO: change Address, call Update(dto)

        // Assert
        // TODO: Get(customerId) and verify Address equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingCustomerId_RemovesLegalCustomerFromDatabase()
    {
        // Arrange
        // TODO: insert Customer + LegalCustomer, capture customerId

        // Act
        // TODO: call Delete(customerId)

        // Assert
        // TODO: Get(customerId) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
