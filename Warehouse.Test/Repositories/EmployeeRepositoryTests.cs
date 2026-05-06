using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class EmployeeRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidEmployee_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: create an EmployeeDto with unique PersonalId (11 chars), FirstName, LastName, Phone, Email

        // Act
        // TODO: call UnitOfWork.EmployeeRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingEmployeeId_ReturnsMatchingEmployee()
    {
        // Arrange
        // TODO: insert an EmployeeDto, capture id

        // Act
        // TODO: call UnitOfWork.EmployeeRepository.Get(id)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.PersonalId, Is.EqualTo(expected value))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingEmployeeId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.EmployeeRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByLastName_ReturnsOnlyMatchingEmployees()
    {
        // Arrange
        // TODO: insert two employees with distinct last names

        // Act
        // TODO: call Load(e => e.LastName == insertedLastName)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(1))
        // TODO: Assert.That(result.First().LastName, Is.EqualTo(insertedLastName))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        // TODO: call Load(e => e.Email == "__nonexistent__@test.com")

        // Assert
        // TODO: Assert.That(result, Is.Empty)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedEmail_PersistsNewEmail()
    {
        // Arrange
        // TODO: insert an employee, Get(id) to retrieve entity

        // Act
        // TODO: change Email, call Update(dto)

        // Assert
        // TODO: Get(id) and verify Email equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingEmployeeId_RemovesEmployeeFromDatabase()
    {
        // Arrange
        // TODO: insert an employee, capture id

        // Act
        // TODO: call Delete(id)

        // Assert
        // TODO: Get(id) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
