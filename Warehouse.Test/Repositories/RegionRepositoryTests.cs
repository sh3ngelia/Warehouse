using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class RegionRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidRegion_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: create a RegionDto with a unique Name

        // Act
        // TODO: call UnitOfWork.RegionRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingRegionId_ReturnsMatchingRegion()
    {
        // Arrange
        // TODO: insert a RegionDto, capture id

        // Act
        // TODO: call UnitOfWork.RegionRepository.Get(id)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.Name, Is.EqualTo(expected name))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingRegionId_ReturnsNull()
    {
        // Arrange
        // TODO: use int.MaxValue as id

        // Act
        // TODO: call UnitOfWork.RegionRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByRegionName_ReturnsOnlyMatchingRegions()
    {
        // Arrange
        // TODO: insert two regions with distinct names

        // Act
        // TODO: call Load(r => r.Name == insertedName)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(1))
        // TODO: Assert.That(result.First().Name, Is.EqualTo(insertedName))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        // TODO: call Load(r => r.Name == "__nonexistent__")

        // Assert
        // TODO: Assert.That(result, Is.Empty)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedRegionName_PersistsNewName()
    {
        // Arrange
        // TODO: insert a region, Get(id) to retrieve full entity

        // Act
        // TODO: change Name, call Update(dto)

        // Assert
        // TODO: Get(id) and verify Name equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingRegionId_RemovesRegionFromDatabase()
    {
        // Arrange
        // TODO: insert a region, capture id

        // Act
        // TODO: call Delete(id)

        // Assert
        // TODO: Get(id) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
