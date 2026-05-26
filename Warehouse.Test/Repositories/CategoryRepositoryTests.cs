using Warehouse.DTO.Products;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class CategoryRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidCategory_ReturnsNewPositiveId()
    {
        // Arrange
        var repository = UnitOfWork.CategoryRepository;
        CategoryDto newCategory = new CategoryDto
        {
            CategoryName = "Test Category" 
        };

        // Act
        int id = repository.Insert(newCategory);
        CategoryDto? insertedCategory = repository.Get(id);

        // Assert
        Assert.That(id, Is.GreaterThan(0), "Insert should return a positive id");
        Assert.That(insertedCategory, Is.Not.Null, "Inserted category should be retrievable");
        Assert.That(insertedCategory!.CategoryName, Is.EqualTo(newCategory.CategoryName), "Inserted category name should match");
    }

    //[Test]
    //public void Insert_WithInvalidCategory_ReturnsNewPositiveId()
    //{
    //    // Arrange
    //    var repository = UnitOfWork.CategoryRepository;
    //    CategoryDto newCategory = new CategoryDto
    //    {
    //        CategoryName = null
    //    };

    //    // Act and Assert
    //    Assert.That(() => repository.Insert(newCategory), Throws.Exception, "Inserting a category with null name should throw an exception");
    //}

    [Test]
    public void Get_WithExistingCategoryId_ReturnsMatchingCategory()
    {
        // Arrange
        const int id = 1;
        const string expectedName = "Electronics";

        // Act
        var result = UnitOfWork.CategoryRepository.Get(id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result!.CategoryName, Is.EqualTo(expectedName));
    }

    [Test]
    public void Get_WithNonExistingCategoryId_ReturnsNull()
    {
        // Act
        var result = UnitOfWork.CategoryRepository.Get(int.MaxValue);

        // Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByCategoryName_ReturnsOnlyMatchingCategories()
    {
        // Arrange
        const string categoryName = "Electronics";

        // Act
        var result = UnitOfWork.CategoryRepository.Load(c => c.CategoryName == categoryName).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result.First().CategoryName, Is.EqualTo(categoryName));
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Act
        var result = UnitOfWork.CategoryRepository.Load(c => c.CategoryName == "__nonexistent__").ToList();

        // Assert
        Assert.That(result, Is.Empty);
    }

    [Test]
    public void Update_WithChangedCategoryName_PersistsNewName()
    {
        // Arrange
        var dto = UnitOfWork.CategoryRepository.Get(2); // Furniture
        Assert.That(dto, Is.Not.Null);

        dto!.CategoryName = "Furniture_Updated";

        // Act
        UnitOfWork.CategoryRepository.Update(dto);

        // Assert
        var updated = UnitOfWork.CategoryRepository.Get(2);
        Assert.That(updated, Is.Not.Null);
        Assert.That(updated!.CategoryName, Is.EqualTo("Furniture_Updated"));
    }

    [Test]
    public void Delete_WithExistingCategoryId_RemovesCategoryFromDatabase()
    {
        // Arrange
        const int id = 5; // Tools

        // Act
        UnitOfWork.CategoryRepository.Delete(id);

        // Assert
        var result = UnitOfWork.CategoryRepository.Get(id);
        Assert.That(result, Is.Null);
    }
}