using ReqManager.Data.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Services.Estructure
{
    public abstract class ServiceBase<T> where T : class
    {
        #region Constructor

        protected ServiceBase(IRepository<T> repository, IUnitOfWork unit)
        {
            this.repository = repository;
            this.unit = unit;
        }

        #endregion

        #region Properties

        protected IRepository<T> repository { get; set; }
        protected IUnitOfWork unit { get; set; }

        #endregion

        #region Implementation

        public virtual void add(T entity)
        {
            repository.add(entity);
        }

        public virtual void update(T entity)
        {
            repository.update(entity);
        }

        public virtual void delete(T entity)
        {
            repository.delete(entity);
        }

        public virtual void delete(Expression<Func<T, bool>> where)
        {
            repository.delete(where);
        }

        public virtual T get(int? id)
        {
            return repository.get(id);
        }

        public virtual T get(Expression<Func<T, bool>> where)
        {
            return repository.get(where);
        }

        public virtual IEnumerable<T> getAll()
        {
            return repository.getAll();
        }

        public virtual IEnumerable<T> filter(Expression<Func<T, bool>> where)
        {
            return repository.filter(where);
        }

        public virtual void saveChanges()
        {
            unit.Commit();
        }

        #endregion
    }
}
