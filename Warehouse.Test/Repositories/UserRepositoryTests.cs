using System.Text;
using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class UserRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidUser_ReturnsEmployeeId()
    {
        // Arrange
        // TODO: insert an EmployeeDto to get a valid EmployeeId (Users.EmployeeId FK → Employees)
        // TODO: create a UserDto with that EmployeeId, a unique Username, and Password as byte[]
        //       e.g. Password = Encoding.UTF8.GetBytes("testpassword")

        // Act
        // TODO: call UnitOfWork.UserRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingEmployeeId_ReturnsMatchingUser()
    {
        // Arrange
        // TODO: insert Employee + User, capture employeeId

        // Act
        // TODO: call UnitOfWork.UserRepository.Get(employeeId)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.Username, Is.EqualTo(expected username))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingEmployeeId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.UserRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByUsername_ReturnsOnlyMatchingUsers()
    {
        // Arrange
        // TODO: insert two Employee + User pairs with distinct usernames

        // Act
        // TODO: call Load(u => u.Username == insertedUsername)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(1))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedUsername_PersistsNewUsername()
    {
        // Arrange
        // TODO: insert Employee + User, Get(employeeId) to retrieve entity

        // Act
        // TODO: change Username, call Update(dto)

        // Assert
        // TODO: Get(employeeId) and verify Username equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingEmployeeId_RemovesUserFromDatabase()
    {
        // Arrange
        // TODO: insert Employee + User, capture employeeId

        // Act
        // TODO: call Delete(employeeId)

        // Assert
        // TODO: Get(employeeId) and verify result is null
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void IsLoginUnique_CurrentlyThrowsNotImplementedException()
    {
        // This test documents the known gap: IsLoginUnique is not implemented.
        // Replace this test with a real implementation once the method is written.

        // Arrange
        // TODO: insert Employee + User with a known username

        // Act & Assert
        // TODO: Assert.Throws<NotImplementedException>(() => UnitOfWork.UserRepository.IsLoginUnique("someUsername"))
        //       Once implemented, remove this test and add:
        //         IsLoginUnique_WithExistingUsername_ReturnsFalse
        //         IsLoginUnique_WithUniqueUsername_ReturnsTrue
        Assert.Ignore("TODO: implement (method currently throws NotImplementedException)");
    }
}
