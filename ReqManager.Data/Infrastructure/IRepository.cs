using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void add(T entity);
        void add(IEnumerable<T> entities);
        void update(T entity);
        void delete(T entity);
        void delete(IEnumerable<T> entities);
        void delete(Expression<Func<T, bool>> where);
        T get(int? id);
        T get(Expression<Func<T, bool>> where);
        IEnumerable<T> getAll();
        IEnumerable<T> filter(Expression<Func<T, bool>> where);
    }
}
