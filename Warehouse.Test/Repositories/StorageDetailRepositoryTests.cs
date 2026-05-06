using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class StorageDetailRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidStorageDetail_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: build the full chain:
        //   1. Insert Customer → CustomerId
        //   2. Insert Employee → EmployeeId
        //   3. Insert Contract → ContractId
        //   4. Insert StorageStatus + Region + City + Storage → StorageId
        //   5. Insert ContractDetail(ContractId, StorageId) → ContractDetailId
        //   6. Insert Category + Product → ProductId
        // TODO: create a StorageDetailDto with ContractDetailId, ProductId, Quantity >= 0

        // Act
        // TODO: call UnitOfWork.StorageDetailRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingStorageDetailId_ReturnsMatchingStorageDetail()
    {
        // Arrange
        // TODO: insert full chain + StorageDetail, capture storageDetailId

        // Act
        // TODO: call UnitOfWork.StorageDetailRepository.Get(storageDetailId)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.Quantity, Is.EqualTo(expected quantity))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingStorageDetailId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.StorageDetailRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByContractDetailId_ReturnsAllItemsInThatContractDetail()
    {
        // Arrange
        // TODO: insert full chain, then insert two StorageDetails for the same ContractDetailId

        // Act
        // TODO: call Load(sd => sd.ContractDetailId == contractDetailId)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(2))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedQuantity_PersistsNewQuantity()
    {
        // Arrange
        // TODO: insert full chain + StorageDetail, Get(id) to retrieve entity

        // Act
        // TODO: change Quantity, call Update(dto)

        // Assert
        // TODO: Get(id) and verify Quantity equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingStorageDetailId_RemovesStorageDetailFromDatabase()
    {
        // Arrange
        // TODO: insert full chain + StorageDetail, capture id

        // Act
        // TODO: call Delete(id)

        // Assert
        // TODO: Get(id) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
