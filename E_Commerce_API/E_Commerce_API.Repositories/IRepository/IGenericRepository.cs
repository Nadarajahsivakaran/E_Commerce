using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Commerce_API.DataAccess.IRepository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IEnumerable<T>> GetAll(string? includeProperties = "");

        Task<T> GetData(Expression<Func<T,bool>> predicate, string includeProperties="");

        Task<T> Create(T entity);

        Task<T> Update(T entity);

        Task<bool> Delete(int id);

        Task Save();

        Task<bool> IsValueExit(Expression<Func<T, bool>> predicate);
    }
}
