using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace ReqManager.Data.Infrastructure
{
    public interface IRepository<TModel> where TModel : class
    {
        void Detached(TModel model);
        void add(TModel entity);
        void add(IEnumerable<TModel> entities);
        void update(TModel entity);
        void delete(int? id);
        void delete(List<int> entitiesID);
        void delete(Expression<Func<TModel, bool>> where);
        TModel get(int? id);
        TModel get(Expression<Func<TModel, bool>> where);
        IEnumerable<TModel> getAll(int top = 0);
        IEnumerable<TModel> filter(Expression<Func<TModel, bool>> where);
    }
}
