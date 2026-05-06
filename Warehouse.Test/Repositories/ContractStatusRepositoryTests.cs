using Warehouse.DTO.Lookups;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class ContractStatusRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidContractStatus_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: create a ContractStatusDto with a unique Name
        //       Note: ContractStatusId is byte? — the SP returns it as an output param

        // Act
        // TODO: call UnitOfWork.ContractStatusRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingContractStatusId_ReturnsMatchingStatus()
    {
        // Arrange
        // TODO: insert a ContractStatusDto, capture id

        // Act
        // TODO: call UnitOfWork.ContractStatusRepository.Get(id)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.Name, Is.EqualTo(expected name))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingContractStatusId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.ContractStatusRepository.Get(byte.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByStatusName_ReturnsOnlyMatchingStatuses()
    {
        // Arrange
        // TODO: insert two contract statuses with distinct names

        // Act
        // TODO: call Load(cs => cs.Name == insertedName)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(1))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedStatusName_PersistsNewName()
    {
        // Arrange
        // TODO: insert a status, Get(id) to retrieve entity

        // Act
        // TODO: change Name, call Update(dto)

        // Assert
        // TODO: Get(id) and verify Name equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingContractStatusId_RemovesStatusFromDatabase()
    {
        // Arrange
        // TODO: insert a status, capture id

        // Act
        // TODO: call Delete(id)

        // Assert
        // TODO: Get(id) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
