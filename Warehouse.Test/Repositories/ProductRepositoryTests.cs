using Warehouse.DTO.Products;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class ProductRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidProduct_ReturnsNewPositiveId()
    {
        // Arrange
        var dto = new ProductDto
        {
            CategoryId = 1,
            Name = "Product_" + Guid.NewGuid().ToString("N")[..8],
            SKU = Guid.NewGuid().ToString("N")[..20],
            Description = "Test product"
        };

        // Act
        var id = UnitOfWork.ProductRepository.Insert(dto);

        // Assert
        Assert.That(id, Is.GreaterThan(0));
    }

    [Test]
    public void Get_WithExistingProductId_ReturnsMatchingProduct()
    {
        // Arrange
        const int productId = 1;
        const string expectedName = "Laptop";

        // Act
        var result = UnitOfWork.ProductRepository.Get(productId);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.Name, Is.EqualTo(expectedName));
    }

    [Test]
    public void Get_WithNonExistingProductId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.ProductRepository.Get(int.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByProductName_ReturnsOnlyMatchingProducts()
    {
        // Arrange
        const string productName = "Monitor";

        // Act
        var result = UnitOfWork.ProductRepository.Load(p => p.Name == productName).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo(productName));
    }

    [Test]
    public void Load_ByCategoryId_ReturnsAllProductsInThatCategory()
    {
        // Arrange
        const int categoryId = 1;

        // Act
        var result = UnitOfWork.ProductRepository.Load(p => p.CategoryId == categoryId).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(3));
        Assert.That(result.All(p => p.CategoryId == categoryId), Is.True);
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        var result = UnitOfWork.ProductRepository.Load(p => p.Name == "__nonexistent__").ToList();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Update_WithChangedProductName_PersistsNewName()
    {
        // Arrange
        var dto = UnitOfWork.ProductRepository.Get(4); // Coffee Pack
        Assert.That(dto, Is.Not.Null);

        dto!.Name = "Coffee Pack Updated";

        // Act
        UnitOfWork.ProductRepository.Update(dto);

        // Assert
        var updated = UnitOfWork.ProductRepository.Get(4);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.Name, Is.EqualTo("Coffee Pack Updated"));
    }

    [Test]
    public void Delete_WithExistingProductId_RemovesProductFromDatabase()
    {
        // Arrange
        const int productId = 5; // Jacket

        // Act
        UnitOfWork.ProductRepository.Delete(productId);

        // Assert
        var result = UnitOfWork.ProductRepository.Get(productId);
        Assert.That(result, Is.Null);
    }
}