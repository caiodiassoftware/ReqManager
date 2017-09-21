using AutoMapper;
using ReqManager.Data.Infrastructure;
using ReqManager.Utils.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

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
                cfg.CreateMap<TModel, TEntity>();
                cfg.CreateMap<TEntity, TModel>();
                cfg.AddGlobalIgnore("Model");
            });
        }

        #endregion

        #region Properties

        protected IRepository<TModel> repository { get; set; }
        protected IUnitOfWork unit { get; set; }

        #endregion

        #region Implementation

        public virtual void add(TEntity entity)
        {
            try
            {
                repository.add(convertEntityToModel(entity));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void update(TEntity entity)
        {
            try
            {
                repository.update(convertEntityToModel(entity));
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual void delete(TEntity entity)
        {
            try
            {
                repository.delete(convertEntityToModel(entity));
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
                return convertModelToEntity(repository.get(id));
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

        public virtual IEnumerable<TEntity> getAll()
        {
            try
            {
                Mapper.Initialize(cfg =>
                {
                    cfg.CreateMap<TModel, TEntity>();
                    cfg.CreateMap<TEntity, TModel>();
                    cfg.AddGlobalIgnore("Model");
                    cfg.IgnoreUnmapped();
                });
                return Mapper.Map<IEnumerable<TModel>, IEnumerable<TEntity>>(repository.getAll());
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

        public virtual void saveChanges()
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

        #endregion

        #region Privates Methods

        protected TModel convertEntityToModel(TEntity entity)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TModel, TEntity>();
                cfg.CreateMap<TEntity, TModel>();
                cfg.IgnoreUnmapped();
            });
            var obj = Mapper.Map<TEntity, TModel>(entity);
            return Mapper.Map<TEntity, TModel>(entity);
        }

        protected TEntity convertModelToEntity(TModel model)
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<TModel, TEntity>();
                cfg.CreateMap<TEntity, TModel>();
                cfg.IgnoreUnmapped();
            });
            var obj = Mapper.Map<TModel, TEntity>(model);
            return Mapper.Map<TModel, TEntity>(model);
        }

        #endregion
    }
}
