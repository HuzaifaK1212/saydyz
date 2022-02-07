using System;
using Platform.Data.Model.Configuration;
using Platform.Data.Repositories;

using Microsoft.EntityFrameworkCore;

namespace Platform.Data.Repositories.Context {

    public class ConfigurationContext : BaseDbContext<ConfigurationContext>
    {
        public ConfigurationContext(DbContextOptions<ConfigurationContext> options) : base(options)
        {

        }

        public DbSet<ConfigurationProfile> ConfigurationProfiles { get; set; }
        public DbSet<ConfigurationParameter> ConfigurationParameters { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ConfigurationProfile>().ToTable("ConfigurationProfile");
            modelBuilder.Entity<ConfigurationParameter>().ToTable("ConfigurationParameter");

        }
    }
        
    }