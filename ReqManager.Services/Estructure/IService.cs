using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Estructure
{
    public interface IService<TEntity> where TEntity : class
    {
        void add(TEntity entity);
        void update(TEntity entity);
        void delete(int? id);
        void delete(Expression<Func<TEntity, bool>> where);
        TEntity get(int? id);
        TEntity get(Expression<Func<TEntity, bool>> where);
        IEnumerable<TEntity> getAll();
        IEnumerable<TEntity> filter(Expression<Func<TEntity, bool>> where);
        void saveChanges();
    }
}
