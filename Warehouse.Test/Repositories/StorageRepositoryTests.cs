using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class StorageRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidStorage_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: insert a StorageStatusDto (Main) to get a valid Status id
        // TODO: insert a Region then a City to get a valid CityId
        // TODO: create a StorageDto with those FK values, unique Name, Address, Capacity, Price

        // Act
        // TODO: call UnitOfWork.StorageRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingStorageId_ReturnsMatchingStorage()
    {
        // Arrange
        // TODO: insert StorageStatus + Region + City + Storage, capture storageId

        // Act
        // TODO: call UnitOfWork.StorageRepository.Get(storageId)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.Name, Is.EqualTo(expected name))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingStorageId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.StorageRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByStorageName_ReturnsOnlyMatchingStorages()
    {
        // Arrange
        // TODO: insert StorageStatus + Region + City + two Storages with distinct names

        // Act
        // TODO: call Load(s => s.Name == insertedName)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(1))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByCityId_ReturnsAllStoragesInThatCity()
    {
        // Arrange
        // TODO: insert StorageStatus + Region + City + two Storages linked to same CityId

        // Act
        // TODO: call Load(s => s.CityId == cityId)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(2))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        // TODO: call Load(s => s.Name == "__nonexistent__")

        // Assert
        // TODO: Assert.That(result, Is.Empty)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedStoragePrice_PersistsNewPrice()
    {
        // Arrange
        // TODO: insert full chain, Get(storageId) to retrieve entity

        // Act
        // TODO: change Price, call Update(dto)

        // Assert
        // TODO: Get(storageId) and verify Price equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingStorageId_RemovesStorageFromDatabase()
    {
        // Arrange
        // TODO: insert full chain, capture storageId

        // Act
        // TODO: call Delete(storageId)

        // Assert
        // TODO: Get(storageId) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
