using AutoMapper;
using ReqManager.Data.Infrastructure;
using ReqManager.Services.Extensions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;

namespace ReqManager.Services.Estructure
{
    public abstract class ServiceBase<TModel, TEntity> where TModel : class where TEntity : class
    {
        #region Constructor

        protected ServiceBase(IRepository<TModel> repository, IUnitOfWork unit)
        {
            this.repository = repository;
            this.unit = unit;
            Mapper.Initialize(cfg =>
            {
                cfg.AddGlobalIgnore("DisplayName");
                cfg.CreateAutomaticMapping<TModel, TEntity>();
            });
        }

        #endregion

        #region Properties

        protected IRepository<TModel> repository { get; set; }
        protected IUnitOfWork unit { get; set; }

        #endregion

        #region Implementation

        public virtual void add(ref TEntity entity, bool persistir = true)
        {
            try
            {
                setCreationDate(ref entity);
                TModel model = convertEntityToModel(entity);
                repository.add(model);
                commit(persistir);
                entity = convertModelToEntity(model);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void update(ref TEntity entity, bool persistir = true)
        {
            try
            {
                TModel model = convertEntityToModel(entity);
                repository.update(model);
                commit(persistir);
                entity = convertModelToEntity(model);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void delete(int? id, bool persistir = true)
        {
            try
            {
                repository.delete(id);
                commit(persistir);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void delete(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                Expression<Func<TModel, bool>> newWhere = Mapper.Map<Expression<Func<TModel, bool>>>(where);
                repository.delete(newWhere);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual TEntity get(int? id)
        {
            try
            {
                TModel model = repository.get(id);
                TEntity entity = convertModelToEntity(model);
                repository.Detached(model);
                return entity;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual TEntity get(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                Expression<Func<TModel, bool>> newWhere = Mapper.Map<Expression<Func<TModel, bool>>>(where);
                return Mapper.Map<TModel, TEntity>(repository.get(newWhere));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<TEntity> getAll(int top = 0)
        {
            try
            {
                return convertEnumerableModelToEntity(repository.getAll(top));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<TEntity> filter(Expression<Func<TEntity, bool>> where)
        {
            try
            {
                Expression<Func<TModel, bool>> newWhere = Mapper.Map<Expression<Func<TModel, bool>>>(where);
                return Mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(repository.filter(newWhere));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual IEnumerable<TEntity> filterByValue(string value)
        {
            try
            {
                return Mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(repository.filterByValue(value));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Privates Methods

        protected void setCreationDate(ref TEntity entity)
        {
            try
            {
                PropertyInfo prop = entity.GetType().
                    GetProperty("creationDate",
                    BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                    prop.SetValue(entity, DateTime.Now, null);
                prop = entity.GetType().
                    GetProperty("CreationDate",
                    BindingFlags.Public | BindingFlags.Instance);
                if (null != prop && prop.CanWrite)
                    prop.SetValue(entity, DateTime.Now, null);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected TModel convertEntityToModel(TEntity entity)
        {
            return Mapper.Map<TEntity, TModel>(entity);
        }

        protected TEntity convertModelToEntity(TModel model)
        {
            return Mapper.Map<TModel, TEntity>(model);
        }

        protected IEnumerable<TEntity> convertEnumerableModelToEntity(IEnumerable<TModel> models)
        {
            try
            {
                List<TEntity> entities = new List<TEntity>();
                foreach (TModel item in models)
                    entities.Add(convertModelToEntity(item));
                return entities;
                //return Mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(models);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void commit(bool persistir)
        {
            if (persistir)
                unit.Commit();
            else
                unit.SaveChanges();
        }

        #endregion

        #region Entity Transactions

        protected void BeginTransaction()
        {
            try
            {
                unit.BeginTransaction();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Commit()
        {
            try
            {
                unit.Commit();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected void Rollback()
        {
            try
            {
                unit.Rollback();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion
    }
}
