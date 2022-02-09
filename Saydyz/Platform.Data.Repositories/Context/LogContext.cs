using Microsoft.EntityFrameworkCore;
using Platform.Data.Model.Logs;

namespace Platform.Data.Repositories.Context
{
    public class LogContext : BaseDbContext<LogContext>
    {
        public DbSet<LogRequest> LogRequests { get; set; }
        public DbSet<LogMsg> LogMsgs { get; set; }

        public LogContext(DbContextOptions<LogContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LogRequest>().ToTable("LogRequest");
            modelBuilder.Entity<LogMsg>().ToTable("LogMsg");

            base.OnModelCreating(modelBuilder);
        }
    }
}
