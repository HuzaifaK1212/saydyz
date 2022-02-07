using Platform.Data.Model;
using Platform.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;

namespace Platform.Data.Repositories
{
    public class BaseRepository<TEntity, TContext> : IRepository<TEntity>

         where TEntity : BaseEntity
         where TContext : IDbContext
    {
        protected IDbContext Context;
        protected DbSet<TEntity> Set;

        protected IUnitOfWork<TContext> _uow;

        public BaseRepository(IUnitOfWork<TContext> uow)
        {
            _uow = uow;
            Context = uow.Context;
            Set = Context.Set<TEntity>();
        }

        public virtual TEntity GetById(int id)
        {
            return Set.Find(id);
        }


        public virtual IQueryable<TEntity> GetAll()
        {
            return Set;
        }

        public virtual IQueryable<TEntity> AllIncluding(params Expression<Func<TEntity, object>>[] includeProperties)
        {
            IQueryable<TEntity> query = GetAll();
            foreach (var includeProperty in includeProperties)
                query.Include(includeProperty);

            return query;
        }

        public virtual void Add(TEntity entity)
        {
            Set.Add(entity);
        }

        public virtual void Attach(TEntity entity)
        {
            //applies only on same entity
            if (!Set.Contains(entity))
                Set.Attach(entity);
        }

        public virtual void Update(TEntity entity)
        {
            //applies only on entity plus its child tables
            Set.Update(entity);
        }

        public virtual void Delete(int id)
        {
            var entity = Set.Find(id);
            if (entity != null)
            {
                entity.Active = false;
                Set.Update(entity);
            }
        }

        public virtual void DeletePermenent(int id)
        {
            var entity = Set.Find(id);
            if (entity != null)
            {
                Set.Remove(entity);
            }
        }

        public virtual void Delete(TEntity entity)
        {
            entity.Active = false;
            Set.Update(entity);
        }

        public virtual IUnitOfWork<Context> GetUow<Context>() where Context : IDbContext
        {
            if (typeof(Context) != typeof(TContext))
            {
                throw new Exception("Parameter type should be " + typeof(TContext).ToString());
            }

            return (IUnitOfWork<Context>)_uow;
        }
    }
}