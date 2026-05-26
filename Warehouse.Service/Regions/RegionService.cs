using Warehouse.DTO.Locations;
using Warehouse.Repository.UnitOfWork;
using Warehouse.Service.Abstracts;

namespace Warehouse.Service.Regions;

internal class RegionService : CrudService<RegionDto>, IRegionService
{
    private readonly IUnitOfWork _uow;

    public RegionService(IUnitOfWork uow) : base(uow.RegionRepository)
    {
        _uow = uow;
    }

    public IEnumerable<CustomerDto> GetAll(bool includeDeleted) =>
        _uow.CustomerRepository.Load(c => includeDeleted ? c.CustomerId > 0 : c.IsDeleted == false);

    public CityDto? GetCity(int cityId) =>
        _uow.CityRepository.Get(cityId);

    public IEnumerable<CityDto> GetAllCities()
    {
        return _uow.CityRepository.Load(c => c.CityId > 0);
    }
}