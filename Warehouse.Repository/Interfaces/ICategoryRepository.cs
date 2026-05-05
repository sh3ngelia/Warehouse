using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse.DTO.Main;
using Warehouse.Repository.Repositories;

namespace Warehouse.Repository.Interfaces
{
    public interface ICategoryRepository : IBaseRepository<CategoryDto>
    {
    }
}



