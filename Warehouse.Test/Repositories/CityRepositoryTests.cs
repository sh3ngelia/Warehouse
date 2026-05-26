using System.Linq;
using Warehouse.DTO.Locations;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class CityRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidCity_ReturnsNewPositiveId()
    {
        // Arrange
        var dto = new CityDto
        {
            RegionId = 1,
            Name = "TestCity_" + Guid.NewGuid().ToString("N")[..8]
        };

        // Act
        var id = UnitOfWork.CityRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Get_WithExistingCityId_ReturnsMatchingCity()
    {
        // Arrange
        const int cityId = 1;
        const string expectedName = "Tbilisi";

        // Act
        var result = UnitOfWork.CityRepository.Get(cityId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Get_WithNonExistingCityId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.CityRepository.Get(int.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByCityName_ReturnsOnlyMatchingCities()
    {
        // Arrange
        const string cityName = "Athens";

        // Act
        var result = UnitOfWork.CityRepository.Load(c => c.Name == cityName).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo(cityName));
    }

    [Test]
    public void Load_ByRegionId_ReturnsAllCitiesInThatRegion()
    {
        // Arrange
        const int regionId = 1;

        // Act
        var result = UnitOfWork.CityRepository.Load(c => c.RegionId == regionId).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(4));
        Assert.That(result.All(c => c.RegionId == regionId), Is.True);
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        var result = UnitOfWork.CityRepository.Load(c => c.Name == "__nonexistent__").ToList();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Update_WithChangedCityName_PersistsNewName()
    {
        // Arrange
        var dto = UnitOfWork.CityRepository.Get(3); // Batumi
        Assert.That(dto, Is.Not.Null);

        dto!.Name = "Batumi_Updated";

        // Act
        UnitOfWork.CityRepository.Update(dto);

        // Assert
        var updated = UnitOfWork.CityRepository.Get(3);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.Name, Is.EqualTo("Batumi_Updated"));
    }

    [Test]
    public void Delete_WithExistingCityId_RemovesCityFromDatabase()
    {
        // Arrange
        const int cityId = 12; // Marseille

        // Act
        UnitOfWork.CityRepository.Delete(cityId);

        // Assert
        var result = UnitOfWork.CityRepository.Get(cityId);
        Assert.That(result, Is.Null);
    }
}