using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Platform.Data.Repositories.Interfaces
{
    /**
    * Repository Design Pattern -- Provides an abstraction over DbContext
    * Repository interface specification
    * Define minimum repository functionality
    * */
    public interface IRepository<T>  where T: Model.BaseEntity
    {
        /**
        * Get all records from the table
        * */
        IQueryable<T> GetAll();

        /**
         * Get all record from the table
         * Including linked entitites -- each provided as a parameter
         * */
        IQueryable<T> AllIncluding(params Expression<Func<T, object>>[] includeProperties);

        /**
         * Get one row from the table based on Id
         * It's rapper around Find(id) method of DbSet 
         * */
        T GetById(int id);

        /**
         * Add an entity and related entities to DbSet
         * Does not change anything in db until SaveChanges is called
         * */
        void Add(T entity);

        /**
         * Update an entity and related entities to DbSet
         * Does not change anything in db until SaveChanges is called
         * */
        void Update(T entity);

        /**
         * Delete an entity and related entities to DbSet
         * Does not change anything in db until SaveChanges is called
         * */
        void Delete(T entity);

        /**
         * Delete an entity and related entities to DbSet
         * Does not change anything in db until SaveChanges is called
         * */
        void Delete(int id);

        void DeletePermenent(int id);

        /**
         * Returns associated unit of work object
         * Which is responsible to run all changes in one transaction
         * */
        IUnitOfWork<TContext> GetUow<TContext>() where TContext : IDbContext;
    }
}
