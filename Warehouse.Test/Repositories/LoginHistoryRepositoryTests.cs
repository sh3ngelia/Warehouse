using System.Text;
using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class LoginHistoryRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidLoginHistory_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: insert Employee + User to get a valid UserId (Users.EmployeeId is the FK)
        // TODO: create a LoginHistoryDto with that UserId and LoginAt = DateTime.UtcNow

        // Act
        // TODO: call UnitOfWork.LoginHistoryRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingLoginHistoryId_ReturnsMatchingRecord()
    {
        // Arrange
        // TODO: insert Employee + User + LoginHistory, capture loginHistoryId

        // Act
        // TODO: call UnitOfWork.LoginHistoryRepository.Get(loginHistoryId)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.UserId, Is.EqualTo(expected userId))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingLoginHistoryId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.LoginHistoryRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByUserId_ReturnsAllLoginEntriesForThatUser()
    {
        // Arrange
        // TODO: insert Employee + User, then insert two LoginHistory records for same UserId

        // Act
        // TODO: call Load(lh => lh.UserId == userId)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(2))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        // TODO: call Load(lh => lh.UserId == int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Empty)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingLoginHistoryId_RemovesRecordFromDatabase()
    {
        // Arrange
        // TODO: insert Employee + User + LoginHistory, capture id

        // Act
        // TODO: call Delete(id)

        // Assert
        // TODO: Get(id) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
