using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Warehouse.Service.Factory
{
    public interface IServiceFactory
    {
        TService GetOrCreate<TService>() where TService : class;
    }
}
