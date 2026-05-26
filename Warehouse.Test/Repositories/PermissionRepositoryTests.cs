using System.Linq;
using Warehouse.DTO.Lookups;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class PermissionRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidPermission_ReturnsNewPositiveId()
    {
        // Arrange
        var dto = new PermissionDto
        {
            Name = "Permission_" + Guid.NewGuid().ToString("N")[..8],
            PermissionKey = (short)new Random().Next(100, 30000)
        };

        // Act
        var id = UnitOfWork.PermissionRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Get_WithExistingPermissionId_ReturnsMatchingPermission()
    {
        // Arrange
        const int id = 1;
        const string expectedName = "Create Contract";

        // Act
        var result = UnitOfWork.PermissionRepository.Get(id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Get_WithNonExistingPermissionId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.PermissionRepository.Get(int.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByPermissionName_ReturnsOnlyMatchingPermissions()
    {
        // Arrange
        const string name = "Edit Contract";

        // Act
        var result = UnitOfWork.PermissionRepository.Load(p => p.Name == name).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo(name));
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        var result = UnitOfWork.PermissionRepository.Load(p => p.Name == "__nonexistent__").ToList();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Update_WithChangedPermissionName_PersistsNewName()
    {
        // Arrange
        var dto = UnitOfWork.PermissionRepository.Get(2);
        Assert.That(dto, Is.Not.Null);

        dto!.Name = "Edit Contract Updated";

        // Act
        UnitOfWork.PermissionRepository.Update(dto);

        // Assert
        var updated = UnitOfWork.PermissionRepository.Get(2);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.Name, Is.EqualTo("Edit Contract Updated"));
    }

    [Test]
    public void Delete_WithExistingPermissionId_RemovesPermissionFromDatabase()
    {
        // Arrange
        const int id = 5;

        // Act
        UnitOfWork.PermissionRepository.Delete(id);

        // Assert
        var result = UnitOfWork.PermissionRepository.Get(id);
        Assert.That(result, Is.Null);
    }
}