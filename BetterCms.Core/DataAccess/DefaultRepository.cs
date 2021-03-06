﻿using System;
using System.Data;
using System.Linq;
using System.Linq.Expressions;
using BetterCms.Core.DataAccess.DataContext;
using BetterCms.Core.Exceptions.DataTier;
using BetterCms.Core.Models;
using NHibernate;
using NHibernate.Linq;

namespace BetterCms.Core.DataAccess
{
    public class DefaultRepository : IRepository, IUnitOfWorkRepository
    {
        private readonly IUnitOfWork defaultUnitOfWork;
        private IUnitOfWork unitOfWork;

        private IUnitOfWork UnitOfWork
        {
            get
            {
                if (unitOfWork != null && !unitOfWork.Disposed)
                {
                    return unitOfWork;
                }

                if (defaultUnitOfWork != null && !defaultUnitOfWork.Disposed)
                {
                    return defaultUnitOfWork;
                }

                throw new DataException(string.Format("Repository {0} has no assigned unit of work or it was disposed.", GetType().Name));
            }
        }

        public DefaultRepository(IUnitOfWork unitOfWork)
        {
            defaultUnitOfWork = unitOfWork;
        }

        public void Use(IUnitOfWork unitOfWorkToUse)
        {
            unitOfWork = unitOfWorkToUse;
        }

        public virtual TEntity AsProxy<TEntity>(Guid id) where TEntity : Entity
        {
            return UnitOfWork.Session.Load<TEntity>(id, LockMode.None);
        }

        public virtual TEntity First<TEntity>(Guid id) where TEntity : Entity
        {
            TEntity entity = FirstOrDefault<TEntity>(id);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), id);
            }

            return entity;
        }

        public virtual TEntity First<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : Entity
        {
            TEntity entity = FirstOrDefault(filter);

            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(TEntity), filter.ToString());
            }

            return entity;
        }

        public virtual TEntity FirstOrDefault<TEntity>(Guid id) where TEntity : Entity
        {
            return AsQueryable<TEntity>().FirstOrDefault(f => f.Id == id);
        }

        public virtual TEntity FirstOrDefault<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : Entity
        {
            return AsQueryable<TEntity>().Where(filter).FirstOrDefault();
        }

        public virtual IQueryable<TEntity> AsQueryable<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : Entity
        {
            return AsQueryable<TEntity>().Where(filter);
        }

        public virtual IQueryable<TEntity> AsQueryable<TEntity>() where TEntity : Entity
        {
            return UnitOfWork.Session.Query<TEntity>().Where(f => !f.IsDeleted);
        }

        public virtual bool Any<TEntity>(Expression<Func<TEntity, bool>> filter) where TEntity : Entity
        {
            return AsQueryable<TEntity>().Where(filter).Any();
        }

        public virtual void Save<TEntity>(TEntity entity) where TEntity : Entity
        {
            UnitOfWork.Session.SaveOrUpdate(entity);
        }

        public virtual void Delete<TEntity>(TEntity entity) where TEntity : Entity
        {
            UnitOfWork.Session.Delete(entity);
        }

        public virtual void Delete<TEntity>(Guid id, int version) where TEntity : Entity
        {
            TEntity entity = AsProxy<TEntity>(id);
            entity.Version = version;
            UnitOfWork.Session.Delete(entity);
        }

        public virtual void Attach<TEntity>(TEntity entity) where TEntity : Entity
        {
            UnitOfWork.Session.Lock(entity, LockMode.None);
        }

        public virtual void Detach<TEntity>(TEntity entity) where TEntity : Entity
        {
            UnitOfWork.Session.Evict(entity);
        }

        public void Refresh<TEntity>(TEntity entity) where TEntity : Entity
        {
            UnitOfWork.Session.Refresh(entity);
        }
    }
}
