using Warehouse.DTO.Customer;
using Warehouse.DTO.Locations;
using Warehouse.Repository.UnitOfWork;
using Warehouse.Service.Abstracts;

namespace Warehouse.Service.Customers
{
    internal class CustomerService : CrudService<CustomerDto>, ICustomerService
    {
        private readonly IUnitOfWork _uow;

        public CustomerService(IUnitOfWork uow) : base(uow.CustomerRepository)
        {
            _uow = uow;
        }

        public int RegisterCustomer(CustomerDto customer, PhysicalCustomerDto? physical, LegalCustomerDto? legal)
        {
            _uow.BeginTransaction();
            try
            {
                var newId = _uow.CustomerRepository.Insert(customer);

                if (physical != null) { physical.CustomerId = newId; _uow.PhysicalCustomerRepository.Insert(physical); }
                if (legal != null) { legal.CustomerId = newId; _uow.LegalCustomerRepository.Insert(legal); }

                _uow.CommitTransaction();
                return newId;
            }
            catch { _uow.RollbackTransaction(); throw; }
        }

        public void UpdateCustomer(CustomerDto customer, PhysicalCustomerDto? physical, LegalCustomerDto? legal)
        {
            _uow.BeginTransaction();
            try
            {
                _uow.CustomerRepository.Update(customer);

                if (physical != null) {_uow.PhysicalCustomerRepository.Update(physical); }
                if (legal != null) {_uow.LegalCustomerRepository.Update(legal); }

                _uow.CommitTransaction();
            }
            catch { _uow.RollbackTransaction(); throw; }
        }

        public void AddPhysicalCustomer(PhysicalCustomerDto customer)
        {
            int newId = _uow.PhysicalCustomerRepository.Insert(customer);
        }
        public void AddLegalCustomer(LegalCustomerDto customer)
        {
            int newId = _uow.LegalCustomerRepository.Insert(customer);
        }

        public PhysicalCustomerDto? GetPhysical(int customerId) =>
            _uow.PhysicalCustomerRepository.Get(customerId);

        public LegalCustomerDto? GetLegal(int customerId) =>
            _uow.LegalCustomerRepository.Get(customerId);
    }
}
