using ReqManager.Data.DataAcess;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Migrations;

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
                dataContext.Set<TModel>().AddOrUpdate(model);
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
                    TModel model = get(id);
                    dbSet.Attach(model);
                    dbSet.Remove(model);
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

        public void Detached(TModel model)
        {
            try
            {
                dataContext.Entry(model).State = System.Data.Entity.EntityState.Detached;
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
                TModel model = dbSet.Find(id);
                return model;
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
                return dbSet.AsNoTracking().Where(where).FirstOrDefault();
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
                return (total != 0) ? dbSet.AsNoTracking().ToList().Take(total) : dbSet.AsNoTracking().ToList();
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
