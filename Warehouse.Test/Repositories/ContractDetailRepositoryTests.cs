using Warehouse.DTO.Contracts;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class ContractDetailRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidContractDetail_ReturnsNewPositiveId()
    {
        // Arrange
        var dto = new ContractDetailDto
        {
            ContractId = 1,
            StorageId = 1,
            Price = 150.00m
        };

        // Act
        var id = UnitOfWork.ContractDetailRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Get_WithExistingContractDetailId_ReturnsMatchingDetail()
    {
        // Arrange
        const int id = 1;

        // Act
        var result = UnitOfWork.ContractDetailRepository.Get(id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.ContractId, Is.EqualTo(1));
        Assert.That(result.StorageId, Is.EqualTo(1));
        Assert.That(result.Price, Is.EqualTo(500m));
    }

    [Test]
    public void Get_WithNonExistingContractDetailId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.ContractDetailRepository.Get(int.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByContractId_ReturnsAllDetailsForThatContract()
    {
        // Arrange
        const int contractId = 1;

        // Act
        var result = UnitOfWork.ContractDetailRepository
            .Load(cd => cd.ContractId == contractId)
            .ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result.All(cd => cd.ContractId == contractId), Is.True);
    }

    [Test]
    public void Load_ByContractIdAndStorageId_ReturnsOnlyExactMatch()
    {
        // Arrange
        const int contractId = 2;
        const int storageId = 2;

        // Act
        var result = UnitOfWork.ContractDetailRepository
            .Load(cd => cd.ContractId == contractId && cd.StorageId == storageId)
            .ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].ContractId, Is.EqualTo(contractId));
        Assert.That(result[0].StorageId, Is.EqualTo(storageId));
    }

    [Test]
    public void Load_ByEitherOfTwoStorageIds_ReturnsBothMatchingDetails()
    {
        // Arrange
        const int firstStorageId = 1;
        const int secondStorageId = 2;

        // Act
        var result = UnitOfWork.ContractDetailRepository
            .Load(cd => cd.StorageId == firstStorageId || cd.StorageId == secondStorageId)
            .ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(3));
        Assert.That(result.Any(cd => cd.StorageId == firstStorageId), Is.True);
        Assert.That(result.Any(cd => cd.StorageId == secondStorageId), Is.True);
    }

    [Test]
    public void Load_ByContractIdAndPriceRange_ReturnsOnlyDetailsWithinRange()
    {
        // Arrange
        const int contractId = 3;

        // Act
        var result = UnitOfWork.ContractDetailRepository
            .Load(cd => cd.ContractId == contractId && cd.Price >= 400m && cd.Price <= 500m)
            .ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Price, Is.EqualTo(450m));
    }

    [Test]
    public void Delete_WithExistingContractDetailId_RemovesDetailFromDatabase()
    {
        // Arrange
        var dto = new ContractDetailDto
        {
            ContractId = 1,
            StorageId = 1,
            Price = 175.00m
        };

        var id = UnitOfWork.ContractDetailRepository.Insert(dto);

        // Act
        UnitOfWork.ContractDetailRepository.Delete(id);

        // Assert
        var result = UnitOfWork.ContractDetailRepository.Get(id);
        Assert.That(result, Is.Null);
    }
}