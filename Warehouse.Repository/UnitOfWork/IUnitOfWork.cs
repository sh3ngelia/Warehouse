using System.Data.Common;
using Warehouse.Repository.Interfaces;

namespace Warehouse.Repository.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    ICategoryRepository CategoryRepository { get; }
    ICityRepository CityRepository { get; }
    IContractDetailRepository ContractDetailRepository { get; }
    IContractRepository ContractRepository { get; }
    IContractStatusRepository ContractStatusRepository { get; }
    ICustomerRepository CustomerRepository { get; }
    IEmployeeRepository EmployeeRepository { get; }
    ILegalCustomerRepository LegalCustomerRepository { get; }
    ILoginHistoryRepository LoginHistoryRepository { get; }
    IPermissionRepository PermissionRepository { get; }
    IPhysicalCustomerRepository PhysicalCustomerRepository { get; }
    IProductRepository ProductRepository { get; }
    IRegionRepository RegionRepository { get; }
    IRoleRepository RoleRepository { get; }
    IStorageDetailRepository StorageDetailRepository { get; }
    IStorageRepository StorageRepository { get; }
    IStorageStatusRepository StorageStatusRepository { get; }
    IUserRepository UserRepository { get; }

    void BeginTransaction();
    void CommitTransaction();
    void RollbackTransaction();
}