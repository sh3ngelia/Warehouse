using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class ProductRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidProduct_ReturnsNewPositiveId()
    {
        // Arrange
        // TODO: insert a CategoryDto first to get a valid CategoryId
        // TODO: create a ProductDto with that CategoryId, a unique Name, and a unique SKU (20 chars max)

        // Act
        // TODO: call UnitOfWork.ProductRepository.Insert(dto)

        // Assert
        // TODO: Assert.That(id, Is.GreaterThan(0))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithExistingProductId_ReturnsMatchingProduct()
    {
        // Arrange
        // TODO: insert a Category + Product, capture productId

        // Act
        // TODO: call UnitOfWork.ProductRepository.Get(productId)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.Name, Is.EqualTo(expected name))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingProductId_ReturnsNull()
    {
        // Act
        // TODO: call UnitOfWork.ProductRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByProductName_ReturnsOnlyMatchingProducts()
    {
        // Arrange
        // TODO: insert a Category, then insert two products with distinct names

        // Act
        // TODO: call Load(p => p.Name == insertedName)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(1))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByCategoryId_ReturnsAllProductsInThatCategory()
    {
        // Arrange
        // TODO: insert a Category, then insert two products linked to it

        // Act
        // TODO: call Load(p => p.CategoryId == categoryId)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(2))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        // TODO: call Load(p => p.Name == "__nonexistent__")

        // Assert
        // TODO: Assert.That(result, Is.Empty)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedProductName_PersistsNewName()
    {
        // Arrange
        // TODO: insert Category + Product, Get(productId) to retrieve entity

        // Act
        // TODO: change Name, call Update(dto)

        // Assert
        // TODO: Get(productId) and verify Name equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingProductId_RemovesProductFromDatabase()
    {
        // Arrange
        // TODO: insert Category + Product, capture productId

        // Act
        // TODO: call Delete(productId)

        // Assert
        // TODO: Get(productId) and verify result is null
        Assert.Ignore("TODO: implement");
    }
}
