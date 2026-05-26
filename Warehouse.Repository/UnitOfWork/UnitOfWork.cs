using System.Data;
using System.Data.Common;
using System.Runtime.CompilerServices;
using Warehouse.Repository.Interfaces;
using Warehouse.Repository.Repositories;

namespace Warehouse.Repository.UnitOfWork;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly DbConnection _connection;
    private DbTransaction? _transaction;
    private bool _disposed;
    private bool _wasOpend = true;

    #region Repository Fields

    private readonly Lazy<CategoryRepository> _categoryRepository;
    private readonly Lazy<CityRepository> _cityRepository;
    private readonly Lazy<ContractRepository> _contractRepository;
    private readonly Lazy<ContractDetailRepository> _contractDetailRepository;
    private readonly Lazy<ContractStatusRepository> _contractStatusRepository;
    private readonly Lazy<CustomerRepository> _customerRepository;
    private readonly Lazy<EmployeeRepository> _employeeRepository;
    private readonly Lazy<LegalCustomerRepository> _legalCustomerRepository;
    private readonly Lazy<LoginHistoryRepository> _loginHistoryRepository;
    private readonly Lazy<PermissionRepository> _permissionRepository;
    private readonly Lazy<PhysicalCustomerRepository> _physicalCustomerRepository;
    private readonly Lazy<ProductRepository> _productRepository;
    private readonly Lazy<RegionRepository> _regionRepository;
    private readonly Lazy<RoleRepository> _roleRepository;
    private readonly Lazy<StorageDetailRepository> _storageDetailRepository;
    private readonly Lazy<StorageRepository> _storageRepository;
    private readonly Lazy<StorageStatusRepository> _storageStatusRepository;
    private readonly Lazy<UserRepository> _userRepository;

    #endregion

    public UnitOfWork(DbConnection connection)
    {
        _connection = connection ?? throw new ArgumentNullException(nameof(connection));
        _categoryRepository = new Lazy<CategoryRepository>(() => new CategoryRepository(_connection, GetCurrentTransaction));
        _cityRepository = new Lazy<CityRepository>(() => new CityRepository(_connection, GetCurrentTransaction));
        _contractRepository = new Lazy<ContractRepository>(() => new ContractRepository(_connection, GetCurrentTransaction));
        _contractDetailRepository = new Lazy<ContractDetailRepository>(() => new ContractDetailRepository(_connection, GetCurrentTransaction));
        _contractStatusRepository = new Lazy<ContractStatusRepository>(() => new ContractStatusRepository(_connection, GetCurrentTransaction));
        _customerRepository = new Lazy<CustomerRepository>(() => new CustomerRepository(_connection, GetCurrentTransaction));
        _employeeRepository = new Lazy<EmployeeRepository>(() => new EmployeeRepository(_connection, GetCurrentTransaction));
        _legalCustomerRepository = new Lazy<LegalCustomerRepository>(() => new LegalCustomerRepository(_connection, GetCurrentTransaction));
        _loginHistoryRepository = new Lazy<LoginHistoryRepository>(() => new LoginHistoryRepository(_connection, GetCurrentTransaction));
        _permissionRepository = new Lazy<PermissionRepository>(() => new PermissionRepository(_connection, GetCurrentTransaction));
        _physicalCustomerRepository = new Lazy<PhysicalCustomerRepository>(() => new PhysicalCustomerRepository(_connection, GetCurrentTransaction));
        _productRepository = new Lazy<ProductRepository>(() => new ProductRepository(_connection, GetCurrentTransaction));
        _regionRepository = new Lazy<RegionRepository>(() => new RegionRepository(_connection, GetCurrentTransaction));
        _roleRepository = new Lazy<RoleRepository>(() => new RoleRepository(_connection, GetCurrentTransaction));
        _storageDetailRepository = new Lazy<StorageDetailRepository>(() => new StorageDetailRepository(_connection, GetCurrentTransaction));
        _storageRepository = new Lazy<StorageRepository>(() => new StorageRepository(_connection, GetCurrentTransaction));
        _storageStatusRepository = new Lazy<StorageStatusRepository>(() => new StorageStatusRepository(_connection, GetCurrentTransaction));
        _userRepository = new Lazy<UserRepository>(() => new UserRepository(_connection, GetCurrentTransaction));
    }

    #region Repository Properties

    public ICategoryRepository CategoryRepository => _categoryRepository.Value;
    public ICityRepository CityRepository => _cityRepository.Value;
    public IContractRepository ContractRepository => _contractRepository.Value;
    public IContractDetailRepository ContractDetailRepository => _contractDetailRepository.Value;
    public IContractStatusRepository ContractStatusRepository => _contractStatusRepository.Value;
    public ICustomerRepository CustomerRepository => _customerRepository.Value;
    public IEmployeeRepository EmployeeRepository => _employeeRepository.Value;
    public ILegalCustomerRepository LegalCustomerRepository => _legalCustomerRepository.Value;
    public ILoginHistoryRepository LoginHistoryRepository => _loginHistoryRepository.Value;
    public IPermissionRepository PermissionRepository => _permissionRepository.Value;
    public IPhysicalCustomerRepository PhysicalCustomerRepository => _physicalCustomerRepository.Value;
    public IProductRepository ProductRepository => _productRepository.Value;
    public IRegionRepository RegionRepository => _regionRepository.Value;
    public IRoleRepository RoleRepository => _roleRepository.Value;
    public IStorageDetailRepository StorageDetailRepository => _storageDetailRepository.Value;
    public IStorageRepository StorageRepository => _storageRepository.Value;
    public IStorageStatusRepository StorageStatusRepository => _storageStatusRepository.Value;
    public IUserRepository UserRepository => _userRepository.Value;

    #endregion

    public void BeginTransaction()
    {
        if (_transaction != null)
            throw new InvalidOperationException("A transaction is already in progress.");
        if (_connection.State == ConnectionState.Closed)
        {
            _connection.Open();
            _wasOpend = false;
        }
        _transaction = _connection.BeginTransaction();
    }

    public void CommitTransaction() => HandleTransaction(() => _transaction?.Commit());

    public void RollbackTransaction() => HandleTransaction(() => _transaction?.Rollback());

    public void Dispose()
    {
        if (_disposed)
            return;

        _transaction?.Dispose();
        _disposed = true;
    }

    private void HandleTransaction(Action action)
    {
        if (_transaction == null)
            throw new InvalidOperationException("No active transaction.");
        action();
        _transaction.Dispose();
        _transaction = null;
        if (!_wasOpend)
        {
            _connection.Close();
            _wasOpend = true;
        }
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    private DbTransaction? GetCurrentTransaction() => _transaction;
}