using Warehouse.DTO.Enums;
using Warehouse.DTO.Storage;
using Warehouse.Repository.UnitOfWork;
using Warehouse.Service.Abstracts;

namespace Warehouse.Service.Storages;

internal class StorageService : CrudService<StorageDto>, IStorageService
{
    private readonly IUnitOfWork _uow;

    public StorageService(IUnitOfWork uow) : base(uow.StorageRepository)
    {
        _uow = uow;
    }

    public void ChangeStatus(int storageId, StorageStatus status)
    {
        var storage = _uow.StorageRepository.Get(storageId)
                      ?? throw new InvalidOperationException($"Storage {storageId} not found.");
        storage.Status = (int)status;
        _uow.StorageRepository.Update(storage);
    }
}