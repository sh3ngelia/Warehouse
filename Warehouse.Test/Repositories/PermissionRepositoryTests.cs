using Warehouse.DTO.Lookups;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class PermissionRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidPermission_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: create a PermissionDto with a unique Name and a unique PermissionKey (short)

        // Act
        // TODO: call UnitOfWork.PermissionRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingPermissionId_ReturnsMatchingPermission()
    {
        // Arrange
        // TODO: insert a PermissionDto, capture id

        // Act
        // TODO: call UnitOfWork.PermissionRepository.Get(id)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.Name, Is.EqualTo(expected name))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingPermissionId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.PermissionRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByPermissionName_ReturnsOnlyMatchingPermissions()
    {
        // Arrange
        // TODO: insert two permissions with distinct names

        // Act
        // TODO: call Load(p => p.Name == insertedName)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(1))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        // TODO: call Load(p => p.Name == "__nonexistent__")

        // Assert
        // TODO: Assert.That(result, Is.Empty)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedPermissionName_PersistsNewName()
    {
        // Arrange
        // TODO: insert a permission, Get(id) to retrieve entity

        // Act
        // TODO: change Name, call Update(dto)

        // Assert
        // TODO: Get(id) and verify Name equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingPermissionId_RemovesPermissionFromDatabase()
    {
        // Arrange
        // TODO: insert a permission, capture id

        // Act
        // TODO: call Delete(id)

        // Assert
        // TODO: Get(id) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
