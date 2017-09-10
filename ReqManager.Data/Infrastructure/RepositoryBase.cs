using ReqManager.Data.DataAcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ReqManager.Data.Infrastructure
{
    public abstract class RepositoryBase<T> where T : class
    {
        #region Constructor

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        #endregion

        #region Properties
        private ReqManagerEntities dataContext;
        private readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected ReqManagerEntities DbContext
        {
            get { return dataContext ?? (dataContext = DbFactory.Init()); }
        }
        #endregion

        #region Implementation
        public virtual void add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void update(T entity)
        {
            dbSet.Attach(entity);
            dataContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual void delete(Expression<Func<T, bool>> where)
        {
            IEnumerable<T> objects = dbSet.Where<T>(where).AsEnumerable();
            foreach (T obj in objects)
                dbSet.Remove(obj);
        }

        public virtual T get(int? id)
        {
            return dbSet.Find(id);
        }

        public T get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault<T>();
        }

        public virtual IEnumerable<T> getAll()
        {
            return dbSet.ToList();
        }

        public virtual IEnumerable<T> filter(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        #endregion

    }
}
