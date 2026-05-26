using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.DTO.Contracts;
using Warehouse.DTO.Storage;
using Warehouse.Repository.UnitOfWork;
using Warehouse.Service.Abstracts;

namespace Warehouse.Service.Contracts
{
    internal class ContractService : CrudService<ContractDto>, IContractService
    {
        private readonly IUnitOfWork _uow;

        public ContractService(IUnitOfWork uow) : base(uow.ContractRepository)
        {
            _uow = uow;
        }

        public void CreateContract(ContractDto contract, List<ContractDetailWithStorageDto> details)
        {
            _uow.BeginTransaction();
            try
            {
                int contractId = _uow.ContractRepository.Insert(contract);

                foreach (var detail in details)
                {
                    int contractDetailId = _uow.ContractDetailRepository.Insert(new ContractDetailDto
                    {
                        ContractId = contractId,
                        StorageId = detail.StorageId,
                        Price = detail.Price
                    });

                    _uow.StorageDetailRepository.Insert(new StorageDetailDto
                    {
                        ContractDetailId = contractDetailId,
                        ProductId = detail.ProductId,
                        Quantity = detail.Quantity
                    });
                }

                _uow.CommitTransaction();
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }

        public void UpdateContract(ContractDto contract)
        {
            _uow.ContractRepository.Update(contract);
        }

        public void DeleteContract(int contractId)
        {
            _uow.ContractRepository.Delete(contractId);
        }
    }

    // ეს DTO აერთიანებს ContractDetail + StorageDetail ინფორმაციას
    public class ContractDetailWithStorageDto
    {
        public int StorageId { get; set; }
        public decimal Price { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
    }
}
