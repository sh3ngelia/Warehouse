using System.Linq;
using Warehouse.DTO.Storage;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class StorageRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidStorage_ReturnsNewPositiveId()
    {
        // Arrange
        var dto = new StorageDto
        {
            Status = 1,
            CityId = 1,
            Name = "Storage_" + Guid.NewGuid().ToString("N")[..8],
            Description = "Test storage",
            Capacity = 100,
            Address = "Address_" + Guid.NewGuid().ToString("N")[..8],
            Price = 99.99m
        };

        // Act
        var id = UnitOfWork.StorageRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Get_WithExistingStorageId_ReturnsMatchingStorage()
    {
        // Arrange
        const int storageId = 1;
        const string expectedName = "Tbilisi Central";

        // Act
        var result = UnitOfWork.StorageRepository.Get(storageId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Get_WithNonExistingStorageId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.StorageRepository.Get(int.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByStorageName_ReturnsOnlyMatchingStorages()
    {
        // Arrange
        const string storageName = "Tbilisi Central";

        // Act
        var result = UnitOfWork.StorageRepository
            .Load(s => s.Name == storageName)
            .ToList();

        // Assert
        Assert.That(result.Count, Is.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo(storageName));
    }

    [Test]
    public void Load_ByCityId_ReturnsAllStoragesInThatCity()
    {
        // Arrange
        const int cityId = 1;

        // Act
        var result = UnitOfWork.StorageRepository.Load(s => s.CityId == cityId).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result.All(s => s.CityId == cityId), Is.True);
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        var result = UnitOfWork.StorageRepository.Load(s => s.Name == "__nonexistent__").ToList();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Update_WithChangedStoragePrice_PersistsNewPrice()
    {
        // Arrange
        var dto = UnitOfWork.StorageRepository.Get(2);
        Assert.That(dto, Is.Not.Null);

        dto!.Price = 123.45m;

        // Act
        UnitOfWork.StorageRepository.Update(dto);

        // Assert
        var updated = UnitOfWork.StorageRepository.Get(2);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.Price, Is.EqualTo(123.45m));
    }

    [Test]
    public void Delete_WithExistingStorageId_RemovesStorageFromDatabase()
    {
        // Arrange
        const int storageId = 5;

        // Act
        UnitOfWork.StorageRepository.Delete(storageId);

        // Assert
        var result = UnitOfWork.StorageRepository.Get(storageId);
        Assert.That(result, Is.Null);
    }
}