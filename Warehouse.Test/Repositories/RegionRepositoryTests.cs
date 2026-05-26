using Warehouse.DTO.Locations;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class RegionRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidRegion_ReturnsNewPositiveId()
    {
        // Arrange
        var regionRepository = UnitOfWork.RegionRepository;
        var region = new RegionDto
        {
            Name = "TestRegion"
        };

        // Act
        var id = regionRepository.Insert(region);
        var insertedRegion = regionRepository.Get(id);

        // Assert
        Assert.That(id, Is.GreaterThan(0), "Insert should return a positive id");
        Assert.That(insertedRegion, Is.Not.Null, "Inserted region should be retrievable");
        Assert.That(insertedRegion!.Name, Is.EqualTo(region.Name), "Inserted region should have the same name");
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    public void Get_WithExistingRegionId_ReturnsMatchingRegion(int id)
    {
        // Arrange
        var regionRepository = UnitOfWork.RegionRepository;

        // Act
        var region = regionRepository.Get(id);

        // Assert
        Assert.That(region, Is.Not.Null, $"Region with id {id} should exist");
    }

    [Test]
    public void Get_WithNonExistingRegionId_ReturnsNull()
    {
        // Arrange
        var regionRepository = UnitOfWork.RegionRepository;

        // Act
        var region = regionRepository.Get(-1);

        // Assert
        Assert.That(region, Is.Null, "Region with non-existing id should be null");
    }

    [Test]
    public void Load_ByRegionName_ReturnsOnlyMatchingRegions()
    {
        // Arrange
        var regionRepository = UnitOfWork.RegionRepository;
        const string existingRegionName = "Georgia";

        // Act
        var result = regionRepository.Load(r => r.Name == existingRegionName).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo(existingRegionName));
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Arrange
        var regionRepository = UnitOfWork.RegionRepository;

        // Act
        var result = regionRepository.Load(r => r.Name == "__nonexistent__").ToList();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Update_WithChangedRegionName_PersistsNewName()
    {
        // Arrange
        var regionRepository = UnitOfWork.RegionRepository;
        var regionToUpdate = regionRepository.Get(3); // USA

        Assert.That(regionToUpdate, Is.Not.Null);

        regionToUpdate!.Name = "USA_Updated";

        // Act
        regionRepository.Update(regionToUpdate);

        // Assert
        var updatedRegion = regionRepository.Get(3);
        Assert.That(updatedRegion, Is.Not.Null);
        Assert.That(updatedRegion!.Name, Is.EqualTo("USA_Updated"));
    }

    [Test]
    public void Delete_WithExistingRegionId_RemovesRegionFromDatabase()
    {
        // Arrange
        var regionRepository = UnitOfWork.RegionRepository;
        const int regionId = 4; // France

        // Act
        regionRepository.Delete(regionId);

        // Assert
        var deletedRegion = regionRepository.Get(regionId);
        Assert.That(deletedRegion, Is.Null);
    }
}