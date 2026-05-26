using Warehouse.DTO.Locations;

namespace Warehouse.Service.Abstracts;

public interface IRegionService : ICrudService<RegionDto>
{
    CityDto? GetCity(int cityId);
    IEnumerable<CityDto> GetAllCities();
}