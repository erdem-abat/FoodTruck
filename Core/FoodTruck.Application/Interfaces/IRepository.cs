using System.Linq.Expressions;

namespace FoodTruck.Application.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T> GetByIdAsync(int id);
    Task CreateAsync(T entity);
    Task UpdateAsync(T entity);
    Task RemoveAsync(T entity);
    Task<T?> GetByFilterAsync(Expression<Func<T, bool>> filter);
}