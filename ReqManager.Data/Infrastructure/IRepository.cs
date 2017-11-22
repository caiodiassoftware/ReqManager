using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReqManager.Data.Infrastructure
{
    public interface IRepository<T> where T : class
    {
        void add(T entity);
        void add(IEnumerable<T> entities);
        void update(T entity);
        void delete(int? id);
        void delete(List<int> entitiesID);
        void delete(Expression<Func<T, bool>> where);
        T get(int? id);
        T get(Expression<Func<T, bool>> where);
        IEnumerable<T> getAll(int total = 0);
        IEnumerable<T> filter(Expression<Func<T, bool>> where);
    }
}
