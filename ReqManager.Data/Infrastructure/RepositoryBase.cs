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
    public abstract class RepositoryBase<TModel> where TModel : class
    {
        #region Constructor

        protected RepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<TModel>();
        }

        #endregion

        #region Properties
        private ReqManagerEntities dataContext;
        private readonly IDbSet<TModel> dbSet;

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
        public virtual void add(TModel model)
        {
            dbSet.Add(model);
        }

        public virtual void add(IEnumerable<TModel> entities)
        {
            foreach (TModel item in entities)
                add(item);
        }

        public virtual void update(TModel model)
        {
            dbSet.Attach(model);
            dataContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
        }

        public virtual void delete(int? id)
        {
            try
            {
                TModel m = dbSet.Find(id);
                dbSet.Remove(m);
            }
            catch (Exception ex)
            {
                throw ex;
            }        
        }

        public virtual void delete(List<int> entitiesID)
        {
            foreach (int id in entitiesID)
                delete(id);
        }

        public virtual void delete(Expression<Func<TModel, bool>> where)
        {
            IEnumerable<TModel> objects = dbSet.Where(where).AsEnumerable();
            foreach (TModel obj in objects)
                dbSet.Remove(obj);
        }

        public virtual TModel get(int? id)
        {
            return dbSet.Find(id);
        }

        public TModel get(Expression<Func<TModel, bool>> where)
        {
            return dbSet.Where(where).FirstOrDefault();
        }

        public virtual IEnumerable<TModel> getAll()
        {
            return dbSet.AsNoTracking().ToList();
        }

        public virtual IEnumerable<TModel> filter(Expression<Func<TModel, bool>> where)
        {
            return dbSet.Where(where).ToList();
        }

        #endregion

    }
}
