using System.Linq.Expressions;
using Warehouse.DTO.Lookups;
using Warehouse.DTO.Users;

namespace Warehouse.Service.Abstracts;

public interface IUserService : ICrudService<UserDto>
{
    public int RegisterEmployee(PersonRegistrationDto person);
}