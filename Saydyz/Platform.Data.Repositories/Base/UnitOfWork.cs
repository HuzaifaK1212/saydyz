using Platform.Data.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Data.Repositories
{
    /**
    * Unit Of Work Design Pattern
    * Responsible to save all changes to database in one transaction
    * Different repositories can share same unit of work object for transation managment
    * */
    public class UnitOfWork<TContext> : IUnitOfWork<TContext> where TContext : IDbContext
    {
        private TContext _context;

        public TContext Context
        {
            get
            {
                return _context;    
            }
        }

        public UnitOfWork(TContext context)
        {
            _context = context;
        }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            if (_context != null)
                _context.Dispose();
        }
    }
}
