using Warehouse.DTO.Lookups;
using Warehouse.DTO.Storage;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class StorageDetailRepositoryTests : RepositoryTestBase
{
    //[Test]
    //public void Insert_WithValidStorageDetail_ReturnsNewPositiveId()
    //{
    //    // Arrange
    //    var dto = new StorageDetailDto
    //    {
    //        ContractDetailId = 5,
    //        ProductId = 2,
    //        Quantity = 100
    //    };

    //    IStorageDetailRepository repository = UnitOfWork.StorageDetailRepository;

    //    // Act
    //    int id = repository.Insert(dto);
    //    StorageDetailDto insertedStorageDetail = repository.Get(id);

    //    // Assert
    //    Assert.That(id, Is.GreaterThan(0));
    //    Assert.That(insertedStorageDetail, Is.Not.Null);
    //    Assert.That(insertedStorageDetail.Quantity, Is.EqualTo(dto.Quantity));
    //}

    [Test]
    public void Get_WithExistingStorageDetailId_ReturnsMatchingStorageDetail()
    {
        // Arrange
        var id = 1;
        // Act
        var storageDetail = UnitOfWork.StorageDetailRepository.Get(id);

        // Assert
        Assert.That(storageDetail, Is.Not.Null);
        Assert.That(storageDetail.StorageDetailId, Is.EqualTo(id));
    }

    [Test]
    public void Get_WithNonExistingStorageDetailId_ReturnsNull() => Assert.That(UnitOfWork.StorageDetailRepository.Get(int.MaxValue), Is.Null);

    [Test]
    public void Load_ByContractDetailId_ReturnsAllItemsInThatContractDetail()
    {
        // Arrange

        // Act
        var result = UnitOfWork.StorageDetailRepository.Load(sd => sd.Quantity > 9 && sd.ContractDetailId == 5);

        // Assert
        Assert.That(result.Count(), Is.EqualTo(2));
    }

    [Test]
    public void Update_WithChangedQuantity_PersistsNewQuantity()
    {
        // Arrange
        var storageDetail = UnitOfWork.StorageDetailRepository.Get(1);
        var newDto = new StorageDetailDto
        {
            StorageDetailId = storageDetail!.StorageDetailId,
            ContractDetailId = storageDetail.ContractDetailId,
            ProductId = storageDetail.ProductId,
            Quantity = storageDetail.Quantity + 100
        };

        // Act
        UnitOfWork.StorageDetailRepository.Update(newDto);
        var updatedStorageDetail = UnitOfWork.StorageDetailRepository.Get(newDto.StorageDetailId!);

        // Assert
        Assert.That(storageDetail, Is.Not.Null);
        Assert.That(storageDetail!.Quantity, Is.Not.EqualTo(updatedStorageDetail!.Quantity));
    }

    [Test]
    public void Delete_WithExistingStorageDetailId_RemovesStorageDetailFromDatabase()
    {
        // Arrange
        var id = 2;
        // Act
        UnitOfWork.StorageDetailRepository.Delete(id);
        var deletedStorageDetail = UnitOfWork.StorageDetailRepository.Get(id);

        // Assert
        Assert.That(deletedStorageDetail, Is.Null);
    }
}
