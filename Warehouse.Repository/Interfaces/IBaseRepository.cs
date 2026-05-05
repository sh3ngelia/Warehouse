using System.Linq.Expressions;

namespace Warehouse.Repository.Interfaces;

public interface IBaseRepository<T> where T : class
{
    T? Get(object id);
    IEnumerable<T> Load(Expression<Func<T, bool>> expression);
    int Insert(T entity);
    void Update(T entity);
    void Delete(object id);
}