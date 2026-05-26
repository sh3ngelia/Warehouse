using Warehouse.DTO.Enums;
using Warehouse.DTO.Storage;

namespace Warehouse.Service.Abstracts;

public interface IStorageService : ICrudService<StorageDto>
{
    void ChangeStatus(int storageId, StorageStatus status);
}