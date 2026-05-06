using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class StorageStatusRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidStorageStatus_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: create a StorageStatusDto (Main namespace) with a unique Name
        //       Note: use Warehouse.DTO.Main.StorageStatusDto — the Lookups duplicate is dead code

        // Act
        // TODO: call UnitOfWork.StorageStatusRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingStorageStatusId_ReturnsMatchingStatus()
    {
        // Arrange
        // TODO: insert a StorageStatusDto, capture id

        // Act
        // TODO: call UnitOfWork.StorageStatusRepository.Get(id)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.Name, Is.EqualTo(expected name))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingStorageStatusId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.StorageStatusRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByStatusName_ReturnsOnlyMatchingStatuses()
    {
        // Arrange
        // TODO: insert two storage statuses with distinct names

        // Act
        // TODO: call Load(s => s.Name == insertedName)

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
    public void Delete_WithExistingStorageStatusId_RemovesStatusFromDatabase()
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
