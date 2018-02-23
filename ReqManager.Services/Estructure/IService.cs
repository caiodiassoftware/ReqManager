using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReqManager.Services.Estructure
{
    public interface IService<TEntity> where TEntity : class
    {
        void add(ref TEntity entity, bool persistir = true);
        void update(ref TEntity entity, bool persistir = true);
        void delete(int? id, bool persistir = true);
        void delete(Expression<Func<TEntity, bool>> where);
        TEntity get(int? id);
        TEntity get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> getAll(int top = 0);
        IEnumerable<TEntity> filter(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> filterByValue(string value);
    }
}
