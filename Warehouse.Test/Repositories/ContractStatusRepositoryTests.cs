using System.Linq;
using Warehouse.DTO.Lookups;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class ContractStatusRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidContractStatus_ReturnsNewPositiveId()
    {
        // Arrange
        var dto = new ContractStatusDto
        {
            Name = "Status_" + Guid.NewGuid().ToString("N")[..8]
        };

        // Act
        var id = UnitOfWork.ContractStatusRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Get_WithExistingContractStatusId_ReturnsMatchingStatus()
    {
        // Arrange
        const int id = 1;
        const string expectedName = "Pending";

        // Act
        var result = UnitOfWork.ContractStatusRepository.Get(id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Get_WithNonExistingContractStatusId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.ContractStatusRepository.Get(byte.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByStatusName_ReturnsOnlyMatchingStatuses()
    {
        // Arrange
        const string expectedName = "Active";

        // Act
        var result = UnitOfWork.ContractStatusRepository.Load(cs => cs.Name == expectedName).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Update_WithChangedStatusName_PersistsNewName()
    {
        // Arrange
        var dto = UnitOfWork.ContractStatusRepository.Get(3); // Completed
        Assert.That(dto, Is.Not.Null);

        dto!.Name = "Completed_New";

        // Act
        UnitOfWork.ContractStatusRepository.Update(dto);

        // Assert
        var updated = UnitOfWork.ContractStatusRepository.Get(3);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.Name, Is.EqualTo("Completed_New"));
    }

    [Test]
    public void Delete_WithExistingContractStatusId_RemovesStatusFromDatabase()
    {
        // Arrange
        const int id = 5; // Suspended

        // Act
        UnitOfWork.ContractStatusRepository.Delete(id);

        // Assert
        var result = UnitOfWork.ContractStatusRepository.Get(id);
        Assert.That(result, Is.Null);
    }
}