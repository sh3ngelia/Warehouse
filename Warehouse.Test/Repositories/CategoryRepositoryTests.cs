using Warehouse.DTO.Main;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class CategoryRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidCategory_ReturnsNewPositiveId()
    {
        // Arrange
        ICategoryRepository repository = UnitOfWork.CategoryRepository;
        CategoryDto newCategory = new CategoryDto
        {
            CategoryName = "Test Category " + Guid.NewGuid()
        };

        // Act
        int id = repository.Insert(newCategory);
        CategoryDto? insertedCategory = repository.Get(id);

        // Assert
        Assert.That(id, Is.GreaterThan(0), "Insert should return a positive id");
        Assert.That(insertedCategory, Is.Not.Null, "Inserted category should be retrievable");
        Assert.That(insertedCategory!.CategoryName, Is.EqualTo(newCategory.CategoryName), "Inserted category name should match");
    }

    [Test]
    public void Insert_WithInvalidCategory_ReturnsNewPositiveId()
    {
        // Arrange
        ICategoryRepository repository = UnitOfWork.CategoryRepository;
        CategoryDto newCategory = new CategoryDto
        {
            CategoryName = null
        };

        // Act and Assert
        Assert.Throws<ArgumentException>(() => repository.Insert(newCategory), "Inserting a category with null name should throw an exception");
    }

    [Test]
    public void Get_WithExistingCategoryId_ReturnsMatchingCategory()
    {
        // Arrange
        // TODO: insert a CategoryDto to get a known id

        // Act
        // TODO: call UnitOfWork.CategoryRepository.Get(id)

        // Assert
        // TODO: Assert.That(result, Is.Not.Null)
        // TODO: Assert.That(result!.CategoryName, Is.EqualTo(expected name))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Get_WithNonExistingCategoryId_ReturnsNull()
    {
        // Arrange
        // TODO: use a large id that is guaranteed not to exist (e.g. int.MaxValue)

        // Act
        // TODO: call UnitOfWork.CategoryRepository.Get(int.MaxValue)

        // Assert
        // TODO: Assert.That(result, Is.Null)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_ByCategoryName_ReturnsOnlyMatchingCategories()
    {
        // Arrange
        // TODO: insert two categories with distinct names

        // Act
        // TODO: call UnitOfWork.CategoryRepository.Load(c => c.CategoryName == insertedName)

        // Assert
        // TODO: Assert.That(result, Has.Count.EqualTo(1))
        // TODO: Assert.That(result.First().CategoryName, Is.EqualTo(insertedName))
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Load_WithPredicateThatMatchesNothing_ReturnsEmptyCollection()
    {
        // Arrange
        // (no setup needed)

        // Act
        // TODO: call Load(c => c.CategoryName == "__nonexistent__")

        // Assert
        // TODO: Assert.That(result, Is.Empty)
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Update_WithChangedCategoryName_PersistsNewName()
    {
        // Arrange
        // TODO: insert a category, capture returned id
        // TODO: Get(id) to get the full entity

        // Act
        // TODO: change CategoryName, call Update(dto)

        // Assert
        // TODO: Get(id) again and verify CategoryName equals the new value
        Assert.Ignore("TODO: implement");
    }

    [Test]
    public void Delete_WithExistingCategoryId_RemovesCategoryFromDatabase()
    {
        // Arrange
        // TODO: insert a category, capture returned id

        // Act
        // TODO: call Delete(id)

        // Assert
        // TODO: Get(id) and verify the result is null (or IsDeleted == true depending on SP behavior)
        Assert.Ignore("TODO: implement");
    }
}
