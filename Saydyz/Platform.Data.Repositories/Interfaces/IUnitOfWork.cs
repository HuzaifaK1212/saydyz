using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Data.Repositories.Interfaces
{
    public interface IUnitOfWork<TContext> : IDisposable where TContext : IDbContext
    {
        /**
        * Commit all changes to database
        * */
        int Commit();

        /**
         * Returns associated db context
         * */
        TContext Context { get; }
    }
}
