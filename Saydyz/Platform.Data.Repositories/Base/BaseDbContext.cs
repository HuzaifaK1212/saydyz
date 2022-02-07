using Platform.Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Platform.Data.Repositories
{
    public class BaseDbContext<T> : DbContext, IDbContext where T : DbContext
    {
        public BaseDbContext(DbContextOptions<T> options) : base(options)
        {}

        public DbContext GetContext()
        {
            return this;
        }

        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
    }
}
