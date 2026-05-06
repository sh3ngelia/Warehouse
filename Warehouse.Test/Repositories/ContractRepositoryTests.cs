using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class ContractRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidContract_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: insert a CustomerDto to get a valid CustomerId
        // TODO: insert an EmployeeDto to get a valid EmployeeId
        // TODO: insert a ContractStatusDto to confirm at least one status exists (or use an existing known id)
        // TODO: create a ContractDto with those FK values and ContractStatus = known status byte

        // Act
        // TODO: call UnitOfWork.ContractRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingContractId_ReturnsMatchingContract()
    {
        // Arrange
        // TODO: insert Customer + Employee + Contract, capture contractId

        // Act
        // TODO: call UnitOfWork.ContractRepository.Get(contractId)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.CustomerId, Is.EqualTo(expected customerId))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingContractId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.ContractRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByCustomerId_ReturnsAllContractsForThatCustomer()
    {
        // Arrange
        // TODO: insert Customer + Employee + two Contracts linked to the same CustomerId

        // Act
        // TODO: call Load(c => c.CustomerId == customerId)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(2))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByEmployeeId_ReturnsAllContractsManagedByThatEmployee()
    {
        // Arrange
        // TODO: insert Customer + Employee + two Contracts linked to the same EmployeeId

        // Act
        // TODO: call Load(c => c.EmployeeId == employeeId)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(2))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        // TODO: call Load(c => c.ContractId == int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Empty)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedContractStatus_PersistsNewStatus()
    {
        // Arrange
        // TODO: insert Customer + Employee + Contract, Get(contractId) to retrieve entity

        // Act
        // TODO: change ContractStatus, call Update(dto)

        // Assert
        // TODO: Get(contractId) and verify ContractStatus equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingContractId_RemovesContractFromDatabase()
    {
        // Arrange
        // TODO: insert Customer + Employee + Contract, capture contractId

        // Act
        // TODO: call Delete(contractId)

        // Assert
        // TODO: Get(contractId) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
