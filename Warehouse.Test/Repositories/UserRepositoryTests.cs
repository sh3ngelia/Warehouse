using System.Linq;
using System.Text;
using Warehouse.DTO;
using Warehouse.DTO.Users;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class UserRepositoryTests : RepositoryTestBase
{
    //[Test]
    //public void Insert_WithValidUser_ReturnsEmployeeId()
    //{
    //    // Arrange
    //    var employeeId = UnitOfWork.EmployeeRepository.Insert(new EmployeeDto
    //    {
    //        PersonalId = Guid.NewGuid().ToString("N")[..11],
    //        FirstName = "User",
    //        LastName = "Employee",
    //        Phone = "5" + Guid.NewGuid().ToString("N")[..11],
    //        Email = $"u{Guid.NewGuid():N}"[..10] + "@t.com"
    //    });

    //    var dto = new UserDto
    //    {
    //        EmployeeId = employeeId,
    //        Username = "user_" + Guid.NewGuid().ToString("N")[..8],
    //        Password = Encoding.UTF8.GetBytes("testpassword")
    //    };

    //    // Act
    //    var id = UnitOfWork.UserRepository.Insert(dto);

    //    // Assert
    //    Assert.That(id, Is.GreaterThan(0));
    //}

    //[Test]
    //public void Get_WithExistingEmployeeId_ReturnsMatchingUser()
    //{
    //    // Arrange
    //    const int employeeId = 1;
    //    const string expectedUsername = "admin";

    //    // Act
    //    var result = UnitOfWork.UserRepository.Get(employeeId);

    //    // Assert
    //    Assert.That(result, Is.Not.Null);
    //    Assert.That(result!.Username, Is.EqualTo(expectedUsername));
    //}

    //[Test]
    //public void Get_WithNonExistingEmployeeId_ReturnsNull()
    //{
    //    // Act
    //    var result = UnitOfWork.UserRepository.Get(int.MaxValue);

    //    // Assert
    //    Assert.That(result, Is.Null);
    //}

    //[Test]
    //public void Load_ByUsername_ReturnsOnlyMatchingUsers()
    //{
    //    // Arrange
    //    const string username = "manager";

    //    // Act
    //    var result = UnitOfWork.UserRepository.Load(u => u.Username == username).ToList();

    //    // Assert
    //    Assert.That(result, Has.Count.EqualTo(1));
    //    Assert.That(result.First().Username, Is.EqualTo(username));
    //}

    //[Test]
    //public void Update_WithChangedUsername_PersistsNewUsername()
    //{
    //    // Arrange
    //    var dto = UnitOfWork.UserRepository.Get(3);
    //    Assert.That(dto, Is.Not.Null);

    //    dto!.Username = "employee1_updated";

    //    // Act
    //    UnitOfWork.UserRepository.Update(dto);

    //    // Assert
    //    var updated = UnitOfWork.UserRepository.Get(3);
    //    Assert.That(updated, Is.Not.Null);
    //    Assert.That(updated!.Username, Is.EqualTo("employee1_updated"));
    //}

    //[Test]
    //public void Delete_WithExistingEmployeeId_RemovesUserFromDatabase()
    //{
    //    // Arrange
    //    const int employeeId = 5;

    //    // Act
    //    UnitOfWork.UserRepository.Delete(employeeId);

    //    // Assert
    //    var result = UnitOfWork.UserRepository.Get(employeeId);
    //    Assert.That(result, Is.Null);
    //}
}