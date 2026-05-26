using Warehouse.DTO.Users;
using Warehouse.Repository.UnitOfWork;
using Warehouse.Service.Abstracts;

namespace Warehouse.Service.Authorization
{
    internal class AuthorizationService : IAuthorizationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public AuthorizationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public UserDto? Login(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrEmpty(password))
                throw new ArgumentException("Username cannot be null or empty.", nameof(username));

            UserDto user;

            try
            {
                user = _unitOfWork.UserRepository.Login(username, password)!;

                var loginHistory = new LoginHistoryDto
                {
                    UserId = user.EmployeeId,
                    LoginAt = DateTime.Now
                };
                _unitOfWork.LoginHistoryRepository.Insert(loginHistory);
            }
            catch
            {
                throw new Exception("Failed to record login history.");
            }

            return user;
        }

        public bool Logout(int userId)
        {
            try
            {
                // Find the latest login record that has no logout
                var loginRecord = _unitOfWork.LoginHistoryRepository
                    .Load(h => h.UserId == userId && h.LogoutAt == null)
                    .OrderByDescending(h => h.LoginAt)
                    .FirstOrDefault();

                if (loginRecord == null) return false;

                loginRecord.LogoutAt = DateTime.Now;
                _unitOfWork.LoginHistoryRepository.Update(loginRecord);
                return true;
            }
            catch
            {
                throw new Exception("Failed to record logout history.");
            }
        }
    }
}
