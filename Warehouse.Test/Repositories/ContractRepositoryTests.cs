using Warehouse.DTO.Contracts;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class ContractRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidContract_ReturnsNewPositiveId()
    {
        // Arrange
        var dto = new ContractDto
        {
            CustomerId = 1,
            EmployeeId = 1,
            ContractStatus = 1
        };

        // Act
        var id = UnitOfWork.ContractRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Get_WithExistingContractId_ReturnsMatchingContract()
    {
        // Arrange
        const int contractId = 1;

        // Act
        var result = UnitOfWork.ContractRepository.Get(contractId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.CustomerId, Is.EqualTo(1));
        Assert.That(result.EmployeeId, Is.EqualTo(1));
        Assert.That(result.ContractStatus, Is.EqualTo((byte)2));
    }

    [Test]
    public void Get_WithNonExistingContractId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.ContractRepository.Get(int.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByCustomerId_ReturnsAllContractsForThatCustomer()
    {
        // Arrange
        const int customerId = 1;

        // Act
        var result = UnitOfWork.ContractRepository.Load(c => c.CustomerId == customerId).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result.All(c => c.CustomerId == customerId), Is.True);
    }

    [Test]
    public void Load_ByEmployeeId_ReturnsAllContractsManagedByThatEmployee()
    {
        // Arrange
        const int employeeId = 1;

        // Act
        var result = UnitOfWork.ContractRepository.Load(c => c.EmployeeId == employeeId).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result.All(c => c.EmployeeId == employeeId), Is.True);
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        var result = UnitOfWork.ContractRepository.Load(c => c.ContractId == int.MaxValue).ToList();

        // Assert
        Assert.That(result, Is.Empty);
    }
}