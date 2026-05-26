using Warehouse.DTO.Contracts;
using Warehouse.DTO.Enums;
using Warehouse.Service.Contracts;

namespace Warehouse.Service.Abstracts;

public interface IContractService : ICrudService<ContractDto>
{
    public void CreateContract(ContractDto contract, List<ContractDetailWithStorageDto> details);
    public void UpdateContract(ContractDto contract);
    public void DeleteContract(int contractId);
}