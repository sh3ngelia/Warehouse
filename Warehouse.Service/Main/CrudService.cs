using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Warehouse.Repository.Interfaces;
using Warehouse.Service.Abstracts;

namespace Warehouse.Service
{
    internal class CrudService<TDto> : ICrudService<TDto> where TDto : class
    {
        private readonly IBaseRepository<TDto> _repository;

        public CrudService(IBaseRepository<TDto> repository)
        {
            _repository = repository;
        }


        public IEnumerable<TDto> GetAll(Expression<Func<TDto, bool>> filter) => _repository.Load(filter);
        public TDto? Get(int id) => _repository.Get(id);
        public int Insert(TDto dto) => _repository.Insert(dto);
        public void Update(TDto dto) => _repository.Update(dto);
        public void Delete(int id) => _repository.Delete(id);

    }

}
