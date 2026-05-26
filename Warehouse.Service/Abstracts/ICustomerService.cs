using Warehouse.DTO.Contracts;
using Warehouse.DTO.Customer;
using Warehouse.DTO.Enums;
using Warehouse.DTO.Locations;

namespace Warehouse.Service.Abstracts;

public interface ICustomerService : ICrudService<CustomerDto>
{
    public void UpdateCustomer(CustomerDto customer, PhysicalCustomerDto? physical, LegalCustomerDto? legal);
    public int RegisterCustomer(CustomerDto customer, PhysicalCustomerDto? physical, LegalCustomerDto? legal);
    public void AddPhysicalCustomer(PhysicalCustomerDto customer);
    public void AddLegalCustomer(LegalCustomerDto customer);
    PhysicalCustomerDto? GetPhysical(int customerId);
    LegalCustomerDto? GetLegal(int customerId);
}