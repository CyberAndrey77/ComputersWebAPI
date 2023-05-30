using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Computers.Repository
{
    public interface IRepository<T> where T : class
    {
        Task AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(Expression<Func<T, bool>> condition);
        Task<T> GetAsync(Expression<Func<T, bool>> condition);
        Task<IEnumerable<T>> GetAllAsync();
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> condition);
        Task<IEnumerable<T>> GetFirstAsync(Expression<Func<T, bool>> condition, int limit);
    }
}
