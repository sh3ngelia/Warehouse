using Warehouse.Repository.UnitOfWork;

namespace Warehouse.Service.Factory
{
    public class ServiceFactory : IServiceFactory
    {
        private UnitOfWork _unitOfWork;
        private readonly Dictionary<Type, object> _singleton = new();

        public ServiceFactory(UnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public TService GetOrCreate<TService>() where TService : class
        {
            var type = typeof(TService);

            if (_singleton.TryGetValue(type, out var cashedService))
            {
                return (TService)cashedService;
            }

            Type implType;
            if (type.IsInterface)
            {
                // ICustomerService → CustomerService
                var implName = type.Name.Substring(1);
                implType = type.Assembly.GetTypes()
                .FirstOrDefault(t => t.Name == implName && type.IsAssignableFrom(t))
                ?? throw new InvalidOperationException($"No implementation found for {type.Name}");
            }
            else
            {
                implType = type;
            }

            var constructor = implType.GetConstructors().FirstOrDefault();

            if (constructor == null)
            {
                throw new InvalidOperationException($"Service {type.Name} must have a constructor with IUnitOfWork parameter.");
            }

            var service = (TService)constructor!.Invoke(new object[] { _unitOfWork });
            _singleton[type] = service;
            return service;
        }
    }
}
