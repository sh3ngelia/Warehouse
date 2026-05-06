using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class PhysicalCustomerRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidPhysicalCustomer_ReturnsCustomerId()
    {
        // Arrange
        // TODO: insert a CustomerDto with CustomerType = false (physical) to get a valid CustomerId
        // TODO: create a PhysicalCustomerDto using that CustomerId, with FirstName, LastName, PersonalId (11 chars)
        //       Note: PhysicalCustomerDto.CustomerId is [IgnoreForInsert] — the SP takes it as a regular param,
        //       so verify how the SP accepts the FK. May need to pass CustomerId manually.

        // Act
        // TODO: call UnitOfWork.PhysicalCustomerRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingCustomerId_ReturnsMatchingPhysicalCustomer()
    {
        // Arrange
        // TODO: insert Customer + PhysicalCustomer, capture customerId

        // Act
        // TODO: call UnitOfWork.PhysicalCustomerRepository.Get(customerId)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.PersonalId, Is.EqualTo(expected value))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingCustomerId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.PhysicalCustomerRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByLastName_ReturnsOnlyMatchingPhysicalCustomers()
    {
        // Arrange
        // TODO: insert two Customer + PhysicalCustomer pairs with distinct last names

        // Act
        // TODO: call Load(p => p.LastName == insertedLastName)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(1))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedLastName_PersistsNewLastName()
    {
        // Arrange
        // TODO: insert Customer + PhysicalCustomer, Get(customerId) to retrieve entity

        // Act
        // TODO: change LastName, call Update(dto)

        // Assert
        // TODO: Get(customerId) and verify LastName equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingCustomerId_RemovesPhysicalCustomerFromDatabase()
    {
        // Arrange
        // TODO: insert Customer + PhysicalCustomer, capture customerId

        // Act
        // TODO: call Delete(customerId)

        // Assert
        // TODO: Get(customerId) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
