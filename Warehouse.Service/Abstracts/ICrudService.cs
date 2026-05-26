using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Warehouse.DTO.Locations;

namespace Warehouse.Service.Abstracts
{
    public interface ICrudService<TDto>
    {
        public IEnumerable<TDto> GetAll(Expression<Func<TDto, bool>> filter);
        TDto? Get(int id);
        int Insert(TDto dto);
        void Update(TDto dto);
        void Delete(int id);
    }
}
