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
