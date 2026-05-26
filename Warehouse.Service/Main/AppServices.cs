using System.Data.Common;
using Warehouse.DTO.Contracts;
using Warehouse.DTO.Customer;
using Warehouse.DTO.Locations;
using Warehouse.DTO.Lookups;
using Warehouse.DTO.Products;
using Warehouse.DTO.Storage;
using Warehouse.DTO.Users;
using Warehouse.Repository.UnitOfWork;
using Warehouse.Service.Abstracts;
using Warehouse.Service.Authorization;
using Warehouse.Service.Contracts;
using Warehouse.Service.Customers;
using Warehouse.Service.Regions;
using Warehouse.Service.Storages;
using Warehouse.Service.Users;

namespace Warehouse.Service.Main
{
    public class AppServices
    {
        // Special services
        private readonly Lazy<ICustomerService> _customers;
        private readonly Lazy<IContractService> _contracts;
        private readonly Lazy<IAuthorizationService> _authorization;
        private readonly Lazy<IStorageService> _storages;
        private readonly Lazy<IPermissionService> _permissions;
        private readonly Lazy<IRegionService> _regions;
        private readonly Lazy<IRoleService> _roles;
        private readonly Lazy<IUserService> _users;

        // CRUD services
        private readonly Lazy<ICrudService<CityDto>> _cities;
        private readonly Lazy<ICrudService<EmployeeDto>> _employees;
        private readonly Lazy<ICrudService<ProductDto>> _products;
        private readonly Lazy<ICrudService<CategoryDto>> _categories;
        private readonly Lazy<ICrudService<ContractStatusDto>> _contractStatuses;
        private readonly Lazy<ICrudService<StorageStatusDto>> _storageStatuses;
        private readonly Lazy<ICrudService<StorageDetailDto>> _storageDetails;
        private readonly Lazy<ICrudService<ContractDetailDto>> _contractDetails;
        private readonly Lazy<ICrudService<LegalCustomerDto>> _legalCustomers;
        private readonly Lazy<ICrudService<PhysicalCustomerDto>> _physicalCustomers;

        // Special service properties
        public ICustomerService Customers => _customers.Value;
        public IContractService Contracts => _contracts.Value;
        public IAuthorizationService Authorization => _authorization.Value;
        public IStorageService Storages => _storages.Value;
        public IPermissionService Permissions => _permissions.Value;
        public IRoleService Roles => _roles.Value;
        public IRegionService Regions => _regions.Value;
        public IUserService Users => _users.Value;

        // CRUD service properties
        public ICrudService<CityDto> Cities => _cities.Value;
        public ICrudService<EmployeeDto> Employees => _employees.Value;
        public ICrudService<ProductDto> Products => _products.Value;
        public ICrudService<CategoryDto> Categories => _categories.Value;
        public ICrudService<ContractStatusDto> ContractStatuses => _contractStatuses.Value;
        public ICrudService<StorageStatusDto> StorageStatuses => _storageStatuses.Value;
        public ICrudService<StorageDetailDto> StorageDetails => _storageDetails.Value;
        public ICrudService<ContractDetailDto> ContractDetails => _contractDetails.Value;
        public ICrudService<LegalCustomerDto> LegalCustomers => _legalCustomers.Value;
        public ICrudService<PhysicalCustomerDto> PhysicalCustomers => _physicalCustomers.Value;

        public AppServices(DbConnection connection)
        {
            var uow = new UnitOfWork(connection);

            // Special services
            _customers = new Lazy<ICustomerService>(() => new CustomerService(uow));
            _contracts = new Lazy<IContractService>(() => new ContractService(uow));
            _authorization = new Lazy<IAuthorizationService>(() => new AuthorizationService(uow));
            _storages = new Lazy<IStorageService>(() => new StorageService(uow));
            _regions = new Lazy<IRegionService>(() => new RegionService(uow));
            _roles = new Lazy<IRoleService>(() => new RoleService(uow));
            _permissions = new Lazy<IPermissionService>(() => new PermissionService(uow));
            _users = new Lazy<IUserService>(() => new UserService(uow));

            // CRUD services
            _cities = new Lazy<ICrudService<CityDto>>(() => new CrudService<CityDto>(uow.CityRepository));
            _employees = new Lazy<ICrudService<EmployeeDto>>(() => new CrudService<EmployeeDto>(uow.EmployeeRepository));
            _products = new Lazy<ICrudService<ProductDto>>(() => new CrudService<ProductDto>(uow.ProductRepository));
            _categories = new Lazy<ICrudService<CategoryDto>>(() => new CrudService<CategoryDto>(uow.CategoryRepository));
            _contractStatuses = new Lazy<ICrudService<ContractStatusDto>>(() => new CrudService<ContractStatusDto>(uow.ContractStatusRepository));
            _storageStatuses = new Lazy<ICrudService<StorageStatusDto>>(() => new CrudService<StorageStatusDto>(uow.StorageStatusRepository));
            _storageDetails = new Lazy<ICrudService<StorageDetailDto>>(() => new CrudService<StorageDetailDto>(uow.StorageDetailRepository));
            _contractDetails = new Lazy<ICrudService<ContractDetailDto>>(() => new CrudService<ContractDetailDto>(uow.ContractDetailRepository));
            _legalCustomers = new Lazy<ICrudService<LegalCustomerDto>>(() => new CrudService<LegalCustomerDto>(uow.LegalCustomerRepository));
            _physicalCustomers = new Lazy<ICrudService<PhysicalCustomerDto>>(() => new CrudService<PhysicalCustomerDto>(uow.PhysicalCustomerRepository));
        }
    }
}