using System.Linq;
using Warehouse.DTO.Lookups;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class RoleRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidRole_ReturnsNewPositiveId()
    {
        // Arrange
        var dto = new RoleDto
        {
            Name = "Role_" + Guid.NewGuid().ToString("N")[..8]
        };

        // Act
        var id = UnitOfWork.RoleRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Get_WithExistingRoleId_ReturnsMatchingRole()
    {
        // Arrange
        const int id = 1;
        const string expectedName = "Admin";

        // Act
        var result = UnitOfWork.RoleRepository.Get(id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Get_WithNonExistingRoleId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.RoleRepository.Get(int.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByRoleName_ReturnsOnlyMatchingRoles()
    {
        // Arrange
        const string roleName = "Manager";

        // Act
        var result = UnitOfWork.RoleRepository.Load(r => r.Name == roleName).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo(roleName));
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        var result = UnitOfWork.RoleRepository.Load(r => r.Name == "__nonexistent__").ToList();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Update_WithChangedRoleName_PersistsNewName()
    {
        // Arrange
        var dto = UnitOfWork.RoleRepository.Get(3); // Employee
        Assert.That(dto, Is.Not.Null);

        dto!.Name = "Employee_Updated";

        // Act
        UnitOfWork.RoleRepository.Update(dto);

        // Assert
        var updated = UnitOfWork.RoleRepository.Get(3);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.Name, Is.EqualTo("Employee_Updated"));
    }

    [Test]
    public void Delete_WithExistingRoleId_RemovesRoleFromDatabase()
    {
        // Arrange
        const int id = 5; // Viewer

        // Act
        UnitOfWork.RoleRepository.Delete(id);

        // Assert
        var result = UnitOfWork.RoleRepository.Get(id);
        Assert.That(result, Is.Null);
    }
}