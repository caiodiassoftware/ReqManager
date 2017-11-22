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
            try
            {
                dbSet.Add(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public virtual void add(IEnumerable<TModel> entities)
        {
            try
            {
                foreach (TModel item in entities)
                    add(item);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void update(TModel model)
        {
            try
            {
                dbSet.Attach(model);
                dataContext.Entry(model).State = System.Data.Entity.EntityState.Modified;
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public virtual void delete(int? id)
        {
            try
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
            catch (Exception ex)
            {
                throw ex;
            }        
        }

        public virtual void delete(List<int> entitiesID)
        {
            try
            {
                foreach (int id in entitiesID)
                    delete(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public virtual void delete(Expression<Func<TModel, bool>> where)
        {
            try
            {
                IEnumerable<TModel> objects = dbSet.Where(where).AsEnumerable();
                foreach (TModel obj in objects)
                    dbSet.Remove(obj);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual TModel get(int? id)
        {
            try
            {
                return dbSet.Find(id);
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public TModel get(Expression<Func<TModel, bool>> where)
        {
            try
            {
                return dbSet.Where(where).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        public virtual IEnumerable<TModel> getAll(int total = 0)
        {
            try
            {
                return (total != 0) ? dbSet.ToList().Take(total) : dbSet.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<TModel> filter(Expression<Func<TModel, bool>> where)
        {
            try
            {
                return dbSet.Where(where).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }            
        }

        #endregion

    }
}
