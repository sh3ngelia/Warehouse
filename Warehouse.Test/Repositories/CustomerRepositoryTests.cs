using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class CustomerRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithPhysicalCustomerType_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: create a CustomerDto with CustomerType = false (physical), unique Phone

        // Act
        // TODO: call UnitOfWork.CustomerRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Insert_WithLegalCustomerType_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: create a CustomerDto with CustomerType = true (legal), unique Phone

        // Act
        // TODO: call UnitOfWork.CustomerRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingCustomerId_ReturnsMatchingCustomer()
    {
        // Arrange
        // TODO: insert a CustomerDto, capture id

        // Act
        // TODO: call UnitOfWork.CustomerRepository.Get(id)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.Phone, Is.EqualTo(expected phone))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingCustomerId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.CustomerRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByCustomerType_ReturnsOnlyMatchingCustomers()
    {
        // Arrange
        // TODO: insert one physical (false) and one legal (true) customer

        // Act
        // TODO: call Load(c => c.CustomerType == false)

        // Assert
        // TODO: verify all returned records have CustomerType == false
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        // TODO: call Load(c => c.Phone == "__nonexistent__")

        // Assert
        // TODO: Assert.That(result, Is.Empty)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedPhone_PersistsNewPhone()
    {
        // Arrange
        // TODO: insert a customer, Get(id) to retrieve entity

        // Act
        // TODO: change Phone, call Update(dto)

        // Assert
        // TODO: Get(id) and verify Phone equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingCustomerId_RemovesCustomerFromDatabase()
    {
        // Arrange
        // TODO: insert a customer, capture id

        // Act
        // TODO: call Delete(id)

        // Assert
        // TODO: Get(id) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
