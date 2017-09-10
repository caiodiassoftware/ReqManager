using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Estructure
{
    public interface IService<T> where T : class
    {
        void add(T entity);
        void update(T entity);
        void delete(T entity);
        void delete(Expression<Func<T, bool>> where);
        T get(int? id);
        T get(Expression<Func<T, bool>> where);
        IEnumerable<T> getAll();
        IEnumerable<T> filter(Expression<Func<T, bool>> where);
        void saveChanges();
    }
}
