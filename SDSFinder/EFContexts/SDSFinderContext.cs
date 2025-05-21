using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using SDSFinder.EFModels;

namespace SDSFinder.EFContexts;

    public partial class SDSFinderContext : DbContext
    {
        public SDSFinderContext()
        {
        }

        public SDSFinderContext(DbContextOptions<SDSFinderContext> options)
            : base(options)
        {
            this.ChangeTracker.LazyLoadingEnabled = true;
        }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {


            OnModelCreatingPartial(modelBuilder);
        }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    public class DocumentDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SDSFinderContext>
    {
        public SDSFinderContext CreateDbContext(string[] args)
        {
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Local")
            {
            }
            else
            {
            }

            return new SDSFinderContext(optionsBuilder.Options);
        }
    }
}

public class DocumentDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SDSFinderContext>
{
    public SDSFinderContext CreateDbContext(string[] args)
    {
        DbContextOptionsBuilder<SDSFinderContext> optionsBuilder = new();
        if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Local")
        {
            optionsBuilder.UseSqlServer("Server=localhost;Database=SDSFinder;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=false");
        }
        else
        {
            optionsBuilder.UseSqlServer("Server=app-db-dev;Database=SDSFinder;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=false");
        }

        return new SDSFinderContext(optionsBuilder.Options);
    }
}
