using Warehouse.DTO.Lookups;
using Warehouse.DTO.Main;

namespace Warehouse.Test.Repositories;

[TestFixture]
public class ContractDetailRepositoryTests : RepositoryTestBase
{
    [Test]
    public void Insert_WithValidContractDetail_ReturnsNewPositiveId()
    {
        // Arrange
        var (contractId, storageId) = CreateParentChain();
        var dto = new ContractDetailDto
        {
            ContractId = contractId,
            StorageId = storageId,
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
        var (contractId, storageId) = CreateParentChain();
        var dto = new ContractDetailDto
        {
            ContractId = contractId,
            StorageId = storageId,
            Price = 150.00m
        };

        var id = UnitOfWork.ContractDetailRepository.Insert(dto);

        // Act
        var result = UnitOfWork.ContractDetailRepository.Get(id);

        // Assert
        Assert.That(result, Is.Not.Null);
        Assert.That(result.ContractId, Is.EqualTo(contractId));
        Assert.That(result.StorageId, Is.EqualTo(storageId));
        Assert.That(result.Price, Is.EqualTo(150.00m));
    }

    [Test]
    public void Get_WithNonExistingContractDetailId_ReturnsNull()
    {
        //Act
        var result = UnitOfWork.ContractDetailRepository.Get(int.MaxValue);

        //Assert
        Assert.That(result, Is.Null);
    }

    [Test]
    public void Load_ByContractId_ReturnsAllDetailsForThatContract()
    {
        // Arrange
        var (contractId, firstStorageId) = CreateParentChain();
        var secondStorageId = CreateStorage();

        UnitOfWork.ContractDetailRepository.Insert(new ContractDetailDto { ContractId = contractId, StorageId = firstStorageId, Price = 100m });
        UnitOfWork.ContractDetailRepository.Insert(new ContractDetailDto { ContractId = contractId, StorageId = secondStorageId, Price = 200m });

        // Act
        var result = UnitOfWork.ContractDetailRepository.Load(cd => cd.ContractId == contractId).ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result.All(cd => cd.ContractId == contractId), Is.True);
    }

    [Test]
    public void Load_ByContractIdAndStorageId_ReturnsOnlyExactMatch()
    {
        // Arrange
        var (contractId, firstStorageId) = CreateParentChain();
        var secondStorageId = CreateStorage();

        var dto1 = new ContractDetailDto { 
            ContractId = contractId, 
            StorageId = firstStorageId,
            Price = 100m 
        };
        var dto2 = new ContractDetailDto { 
            ContractId = contractId, 
            StorageId = secondStorageId, 
            Price = 200m 
        };

        UnitOfWork.ContractDetailRepository.Insert(dto1);
        UnitOfWork.ContractDetailRepository.Insert(dto2);

        // Act — AND condition: same contract, but only one specific storage
        var result = UnitOfWork.ContractDetailRepository
            .Load(cd => cd.ContractId == contractId && cd.StorageId == firstStorageId)
            .ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].StorageId, Is.EqualTo(firstStorageId));
    }

    [Test]
    public void Load_ByEitherOfTwoStorageIds_ReturnsBothMatchingDetails()
    {
        // Arrange
        var (contractId, firstStorageId) = CreateParentChain();
        var secondStorageId = CreateStorage();
        var thirdStorageId = CreateStorage();

        var dto1 = new ContractDetailDto { 
            ContractId = contractId, 
            StorageId = firstStorageId, 
            Price = 100m 
        };
        var dto2 = new ContractDetailDto { 
            ContractId = contractId, 
            StorageId = secondStorageId, 
            Price = 200m 
        };
        var dto3 = new ContractDetailDto { 
            ContractId = contractId, 
            StorageId = thirdStorageId, 
            Price = 300m 
        };

        UnitOfWork.ContractDetailRepository.Insert(dto1);
        UnitOfWork.ContractDetailRepository.Insert(dto2);
        UnitOfWork.ContractDetailRepository.Insert(dto3);

        // Act — OR condition: first OR second storage (third must be excluded)
        var result = UnitOfWork.ContractDetailRepository
            .Load(cd => cd.StorageId == firstStorageId || cd.StorageId == secondStorageId)
            .ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));
        Assert.That(result.Any(cd => cd.StorageId == firstStorageId), Is.True);
        Assert.That(result.Any(cd => cd.StorageId == secondStorageId), Is.True);
    }

    [Test]
    public void Load_ByContractIdAndPriceRange_ReturnsOnlyDetailsWithinRange()
    {
        // Arrange
        var (contractId, firstStorageId) = CreateParentChain();
        var secondStorageId = CreateStorage();
        var thirdStorageId = CreateStorage();

        var dto1 = new ContractDetailDto { 
            ContractId = contractId, 
            StorageId = firstStorageId, 
            Price = 50m 
        };
        var dto2 = new ContractDetailDto { 
            ContractId = contractId, 
            StorageId = secondStorageId, 
            Price = 150m 
        };
        var dto3 = new ContractDetailDto { 
            ContractId = contractId, 
            StorageId = thirdStorageId, 
            Price = 500m 
        };

        UnitOfWork.ContractDetailRepository.Insert(dto1);
        UnitOfWork.ContractDetailRepository.Insert(dto2);
        UnitOfWork.ContractDetailRepository.Insert(dto3);

        var result = UnitOfWork.ContractDetailRepository
            .Load(cd => cd.ContractId == contractId && cd.Price >= 100m && cd.Price <= 200m)
            .ToList();

        // Assert
        Assert.That(result, Has.Count.EqualTo(1));
        Assert.That(result[0].Price, Is.EqualTo(150m));
    }

    [Test]
    public void Delete_WithExistingContractDetailId_RemovesDetailFromDatabase()
    {
        // Arrange
        var (contractId, storageId) = CreateParentChain();
        var id = UnitOfWork.ContractDetailRepository.Insert(new ContractDetailDto { ContractId = contractId, StorageId = storageId, Price = 100m });

        // Act
        UnitOfWork.ContractDetailRepository.Delete(id);

        // Assert
        var result = UnitOfWork.ContractDetailRepository.Get(id);
        Assert.That(result, Is.Null);
    }

    private (int contractId, int storageId) CreateParentChain()
    {
        var contractStatusId = CreateContractStatus();
        var customerId = CreateCustomer();
        var employeeId = CreateEmployee();
        var contractId = CreateContract(customerId, employeeId, contractStatusId);
        var storageId = CreateStorage();

        return (contractId, storageId);
    }

    private int CreateContractStatus()
    {
        var dto = new ContractStatusDto { Name = "ACTIVE" };

        return UnitOfWork.ContractStatusRepository.Insert(dto);
    }

    private int CreateCustomer()
    {
        var dto = new CustomerDto
        {
            CustomerType = false,
            Phone = UniquePhone(),
            Email = UniqueEmail()
        };
        return UnitOfWork.CustomerRepository.Insert(dto);
    }

    private int CreateEmployee()
    {
        var dto = new EmployeeDto
        {
            PersonalId = UniqueId(11),
            FirstName = "Test",
            LastName = "Employee",
            Phone = UniquePhone(),
            Email = UniqueEmail()
        };
        return UnitOfWork.EmployeeRepository.Insert(dto);
    }

    private int CreateContract(int customerId, int employeeId, int contractStatusId)
    {
        var contractDto = new ContractDto
        {
            CustomerId = customerId,
            EmployeeId = employeeId,
            ContractStatus = (byte)contractStatusId
        };
        return UnitOfWork.ContractRepository.Insert(contractDto);
    }

    private int CreateStorage()
    {
        var cityId = CreateCity(CreateRegion());
        var statusId = CreateStorageStatus();

        var dto = new StorageDto
        {
            Status = statusId,
            CityId = cityId,
            Name = Unique(20),
            Capacity = 100.0,
            Address = Unique(30),
            Price = 50.00m
        };
        
        return UnitOfWork.StorageRepository.Insert(dto);
    }

    private int CreateStorageStatus()
    {
        var dto = new StorageStatusDto { Name = Unique(6) };

        return UnitOfWork.StorageStatusRepository.Insert(dto);
    }

    private int CreateRegion()
    {
        var dto = new RegionDto { Name = Unique(20) };

        return UnitOfWork.RegionRepository.Insert(dto);
    }

    private int CreateCity(int regionId) 
    {
        var dto = new CityDto 
        { 
            RegionId = regionId, 
            Name = Unique(20) 
        };

        return UnitOfWork.CityRepository.Insert(dto); 
    }

    private static string Unique(int maxLength)
    {
        var raw = Guid.NewGuid().ToString("N");
        return raw[..Math.Min(maxLength, raw.Length)];
    }

    private static string UniquePhone() => "5" + Guid.NewGuid().ToString("N")[..11];

    private static string UniqueEmail() => $"t{Guid.NewGuid():N}"[..10] + "@t.com";

    private static string UniqueId(int length) => Guid.NewGuid().ToString("N")[..length];

}
