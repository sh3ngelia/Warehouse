using Warehouse.DTO.Users;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class EmployeeRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidEmployee_ReturnsNewPositiveId()
    {
        // Arrange
        var dto = new EmployeeDto
        {
            PersonalId = new string(
                Guid.NewGuid()
                    .ToString("N")
                    .Where(char.IsDigit) 
                    .Take(11)
                    .ToArray()
            ),
            FirstName = "Test",
            LastName = "Employee",
            Phone = "1234567890",
            Email = $"{Guid.NewGuid()}@test.com"
        };

        // Act
        var id = UnitOfWork.EmployeeRepository.Insert(dto);

        Assert.Multiple(() =>
        {
            // Assert
            Assert.That(id, Is.GreaterThan(0));
        });
    }

    [Test]
    public void Get_WithExistingEmployeeId_ReturnsMatchingEmployee()
    {
        // Act
        var expected = "12345678901";
        var result = UnitOfWork.EmployeeRepository.Get(1);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.PersonalId, Is.EqualTo(expected));
    }

    [Test]
    public void Get_WithNonExistingEmployeeId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.EmployeeRepository.Get(int.MaxValue)
        var result = UnitOfWork.EmployeeRepository.Get(int.MaxValue);

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByLastName_ReturnsOnlyMatchingEmployees()
    {
        // Arrange
        var insertedLastName = "Beridze";
        
        // Act
        var result = UnitOfWork.EmployeeRepository.Load(e => 
        e.LastName == insertedLastName && e.IsDeleted == false);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().LastName, Is.EqualTo(insertedLastName));
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Arrange
        var expectedEmail = "__nonexistent__@test.com";
     
        // Act
        var result = UnitOfWork.EmployeeRepository.Load(e => e.Email == expectedEmail);

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Update_WithChangedEmail_PersistsNewEmail()
    {
        // Arrange
        const int employeeId = 3;

        var employee = UnitOfWork.EmployeeRepository.Get(employeeId);
        Assert.That(employee, Is.Not.Null);

        var newEmail = "updated2@test.com";

        employee!.Email = newEmail;

        // Act
        UnitOfWork.EmployeeRepository.Update(employee);
        var updatedEmployee = UnitOfWork.EmployeeRepository.Get(employeeId);

        // Assert
        Assert.That(updatedEmployee, Is.Not.Null);
        Assert.That(updatedEmployee!.Email, Is.EqualTo(newEmail));
    }

    [Test]
    public void Delete_WithExistingEmployeeId_RemovesEmployeeFromDatabase()
    {
        // Act
        UnitOfWork.EmployeeRepository.Delete(2);
        var result = UnitOfWork.EmployeeRepository.Get(2);

        // Assert
        Assert.That(result, Is.Null);
    }
}
