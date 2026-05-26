using System.Linq;
using System.Text;
using Warehouse.DTO.Users;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class LoginHistoryRepositoryTests : RepositoryTestBase
{
    //[Test]
    //public void Insert_WithValidLoginHistory_ReturnsNewPositiveId()
    //{
    //    // Arrange
    //    var employeeId = UnitOfWork.EmployeeRepository.Insert(new EmployeeDto
    //    {
    //        PersonalId = Guid.NewGuid().ToString("N")[..11],
    //        FirstName = "Login",
    //        LastName = "User",
    //        Phone = "5" + Guid.NewGuid().ToString("N")[..11],
    //        Email = $"e{Guid.NewGuid():N}"[..10] + "@t.com"
    //    });

    //    var userId = UnitOfWork.UserRepository.Insert(new UserDto
    //    {
    //        EmployeeId = employeeId,
    //        Username = "user_" + Guid.NewGuid().ToString("N")[..8],
    //        Password = Encoding.UTF8.GetBytes("testpassword")
    //    });

    //    var dto = new LoginHistoryDto
    //    {
    //        UserId = userId,
    //        LoginAt = DateTime.UtcNow
    //    };

    //    // Act
    //    var id = UnitOfWork.LoginHistoryRepository.Insert(dto);

    //    // Assert
    //    Assert.That(id, Is.GreaterThan(0));
    //}

    //[Test]
    //public void Get_WithExistingLoginHistoryId_ReturnsMatchingRecord()
    //{
    //    // Arrange
    //    var employeeId = UnitOfWork.EmployeeRepository.Insert(new EmployeeDto
    //    {
    //        PersonalId = Guid.NewGuid().ToString("N")[..11],
    //        FirstName = "Login",
    //        LastName = "User",
    //        Phone = "5" + Guid.NewGuid().ToString("N")[..11],
    //        Email = $"e{Guid.NewGuid():N}"[..10] + "@t.com"
    //    });

    //    var userId = UnitOfWork.UserRepository.Insert(new UserDto
    //    {
    //        EmployeeId = employeeId,
    //        Username = "user_" + Guid.NewGuid().ToString("N")[..8],
    //        Password = Encoding.UTF8.GetBytes("testpassword")
    //    });

    //    var loginHistoryId = UnitOfWork.LoginHistoryRepository.Insert(new LoginHistoryDto
    //    {
    //        UserId = userId,
    //        LoginAt = DateTime.UtcNow
    //    });

    //    // Act
    //    var result = UnitOfWork.LoginHistoryRepository.Get(loginHistoryId);

    //    // Assert
    //    Assert.That(result, Is.Not.Null);
    //    Assert.That(result!.UserId, Is.EqualTo(userId));
    //}

    //[Test]
    //public void Get_WithNonExistingLoginHistoryId_ReturnsNull()
    //{
    //    // Act
    //    var result = UnitOfWork.LoginHistoryRepository.Get(int.MaxValue);

    //    // Assert
    //    Assert.That(result, Is.Null);
    //}

    //[Test]
    //public void Load_ByUserId_ReturnsAllLoginEntriesForThatUser()
    //{
    //    // Arrange
    //    var employeeId = UnitOfWork.EmployeeRepository.Insert(new EmployeeDto
    //    {
    //        PersonalId = Guid.NewGuid().ToString("N")[..11],
    //        FirstName = "Login",
    //        LastName = "User",
    //        Phone = "5" + Guid.NewGuid().ToString("N")[..11],
    //        Email = $"e{Guid.NewGuid():N}"[..10] + "@t.com"
    //    });

    //    var userId = UnitOfWork.UserRepository.Insert(new UserDto
    //    {
    //        EmployeeId = employeeId,
    //        Username = "user_" + Guid.NewGuid().ToString("N")[..8],
    //        Password = Encoding.UTF8.GetBytes("testpassword")
    //    });

    //    UnitOfWork.LoginHistoryRepository.Insert(new LoginHistoryDto { UserId = userId, LoginAt = DateTime.UtcNow });
    //    UnitOfWork.LoginHistoryRepository.Insert(new LoginHistoryDto { UserId = userId, LoginAt = DateTime.UtcNow.AddMinutes(1) });

    //    // Act
    //    var result = UnitOfWork.LoginHistoryRepository.Load(lh => lh.UserId == userId).ToList();

    //    // Assert
    //    Assert.That(result, Has.Count.EqualTo(2));
    //}

    //[Test]
    //public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    //{
    //    // Act
    //    var result = UnitOfWork.LoginHistoryRepository.Load(lh => lh.UserId == int.MaxValue).ToList();

    //    // Assert
    //    Assert.That(result, Is.Empty);
    //}

    //[Test]
    //public void Delete_WithExistingLoginHistoryId_RemovesRecordFromDatabase()
    //{
    //    // Arrange
    //    var employeeId = UnitOfWork.EmployeeRepository.Insert(new EmployeeDto
    //    {
    //        PersonalId = Guid.NewGuid().ToString("N")[..11],
    //        FirstName = "Login",
    //        LastName = "User",
    //        Phone = "5" + Guid.NewGuid().ToString("N")[..11],
    //        Email = $"e{Guid.NewGuid():N}"[..10] + "@t.com"
    //    });

    //    var userId = UnitOfWork.UserRepository.Insert(new UserDto
    //    {
    //        EmployeeId = employeeId,
    //        Username = "user_" + Guid.NewGuid().ToString("N")[..8],
    //        Password = Encoding.UTF8.GetBytes("testpassword")
    //    });

    //    var id = UnitOfWork.LoginHistoryRepository.Insert(new LoginHistoryDto
    //    {
    //        UserId = userId,
    //        LoginAt = DateTime.UtcNow
    //    });

    //    // Act
    //    UnitOfWork.LoginHistoryRepository.Delete(id);

    //    // Assert
    //    var result = UnitOfWork.LoginHistoryRepository.Get(id);
    //    Assert.That(result, Is.Null);
    //}
}