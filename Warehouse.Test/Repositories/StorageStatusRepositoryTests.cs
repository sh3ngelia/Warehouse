using Warehouse.DTO.Lookups;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class StorageStatusRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidStorageStatus_ReturnsNewPositiveId()
    {
        // Arrange
        // Using Unique(15) because the SQL schema defines Name as varchar(15)
        var dto = new StorageStatusDto
        {
            Name = Unique(15)
        };

        IStorageStatusRepository repository = UnitOfWork.StorageStatusRepository;

        // Act
        int id = repository.Insert(dto);
        var insertedStatus = repository.Get(id);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
        Assert.That(insertedStatus, Is.Not.Null);
        Assert.That(insertedStatus!.Name, Is.EqualTo(dto.Name));
        Assert.That(insertedStatus.IsDeleted, Is.False); // Should be 0 by default
    }

    [Test]
    public void Get_WithExistingStorageStatusId_ReturnsMatchingStatus()
    {
        // Arrange
        var id = 4;

        // Act
        var result = UnitOfWork.StorageStatusRepository.Get(id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.StorageStatusId, Is.EqualTo(id));
    }

    [Test]
    public void Get_WithNonExistingStorageStatusId_ReturnsNull() =>
        Assert.That(UnitOfWork.StorageStatusRepository.Get(int.MaxValue), Is.Null);

    [Test]
    public void Load_ByExactName_ReturnsOnlyMatchingStatuses()
    {
        // Arrange
        // Act
        var result = UnitOfWork.StorageStatusRepository.Load(s => s.Name == "Occupied");

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
    }

    [Test]
    public void Update_WithChangedName_PersistsNewName()
    {
        // Arrange
        var id = 2;
        var existing = UnitOfWork.StorageStatusRepository.Get(id);

        var updateDto = new StorageStatusDto
        {
            StorageStatusId = existing!.StorageStatusId,
            Name = "NewName"
        };

        // Act
        UnitOfWork.StorageStatusRepository.Update(updateDto);
        var updated = UnitOfWork.StorageStatusRepository.Get(id);

        // Assert
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.Name, Is.EqualTo("NewName"));
        Assert.That(updated.UpdateDate, Is.Not.Null);
    }

    [Test]
    public void Delete_WithExistingStorageStatusId_SoftDeletesStatus()
    {
        // Arrange
        var id = 3;

        // Act
        UnitOfWork.StorageStatusRepository.Delete(id);
        var result = UnitOfWork.StorageStatusRepository.Get(id);

        // Assert
        Assert.That(result, Is.Null);
    }


    // Helper for unique strings within varchar constraints
    private static string Unique(int maxLength)
    {
        var raw = Guid.NewGuid().ToString("N");
        return raw[..Math.Min(maxLength, raw.Length)];
    }
}