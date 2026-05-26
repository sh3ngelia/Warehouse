using Warehouse.DTO.Users;
using Warehouse.Repository.UnitOfWork;
using Warehouse.Service.Abstracts;

namespace Warehouse.Service.Users
{
    internal class UserService : CrudService<UserDto>, IUserService
    {
        private readonly IUnitOfWork _uow;

        public UserService(IUnitOfWork uow) : base(uow.UserRepository)
        {
            _uow = uow;
        }
        public int RegisterEmployee(PersonRegistrationDto person)
        {
            _uow.BeginTransaction();
            try
            {
                var employee = new EmployeeDto
                {
                    PersonalId = person.PersonalId,
                    FirstName = person.FirstName,
                    LastName = person.LastName,
                    Phone = person.Phone,
                    Email = person.Email
                };

                int employeeId = _uow.EmployeeRepository.Insert(employee);

                var user = new UserDto
                {
                    EmployeeId = employeeId,
                    Username = person.Username,
                    Password = person.Password
                };

                int userId = _uow.UserRepository.Insert(user);

                _uow.CommitTransaction();
                return userId;
            }
            catch
            {
                _uow.RollbackTransaction();
                throw;
            }
        }
    }
}
