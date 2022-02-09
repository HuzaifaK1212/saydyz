using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Platform.Data.Model.Customer;
using Platform.Data.Model.Flavors;
using Platform.Data.Model.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Platform.Data.Repositories.Context
{
    public class OrderContext : BaseDbContext<OrderContext>
    {
        private readonly IConfiguration configuration;

        public OrderContext(DbContextOptions<OrderContext> options, IConfiguration configuration) : base(options) 
        {
            this.configuration = configuration;

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ConstructDatabaseModel(configuration);
        }
        public DbSet<Flavor> Flavor { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<OrderItem> OrderItem { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<CustomerType> CustomerType { get; set; }
        public DbSet<ItemType> ItemType { get; set; }
    }
}
