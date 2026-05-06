using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class CityRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidCity_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: insert a RegionDto first to get a valid RegionId
        // TODO: create a CityDto with that RegionId and a unique Name

        // Act
        // TODO: call UnitOfWork.CityRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingCityId_ReturnsMatchingCity()
    {
        // Arrange
        // TODO: insert a Region, then insert a City using that RegionId, capture cityId

        // Act
        // TODO: call UnitOfWork.CityRepository.Get(cityId)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.Name, Is.EqualTo(expected name))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingCityId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.CityRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByCityName_ReturnsOnlyMatchingCities()
    {
        // Arrange
        // TODO: insert a Region, then insert two cities with distinct names under that region

        // Act
        // TODO: call Load(c => c.Name == insertedName)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(1))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByRegionId_ReturnsAllCitiesInThatRegion()
    {
        // Arrange
        // TODO: insert a Region, then insert two cities under it

        // Act
        // TODO: call Load(c => c.RegionId == regionId)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(2))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        // TODO: call Load(c => c.Name == "__nonexistent__")

        // Assert
        // TODO: Assert.That(result, Is.Empty)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedCityName_PersistsNewName()
    {
        // Arrange
        // TODO: insert Region + City, Get(cityId) to retrieve entity

        // Act
        // TODO: change Name, call Update(dto)

        // Assert
        // TODO: Get(cityId) and verify Name equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingCityId_RemovesCityFromDatabase()
    {
        // Arrange
        // TODO: insert Region + City, capture cityId

        // Act
        // TODO: call Delete(cityId)

        // Assert
        // TODO: Get(cityId) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
