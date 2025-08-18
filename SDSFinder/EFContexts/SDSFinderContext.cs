using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace SDSFinder.EFContexts;

public partial class SDSFinderContext : DbContext
{
    public SDSFinderContext()
    {
    }

    public SDSFinderContext(DbContextOptions<SDSFinderContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Document> Documents { get; set; }
    
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

        if (!optionsBuilder.IsConfigured)
        {
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Document__3214EC07E93C67FD");

            entity.ToTable("Document");

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FileLocation).HasMaxLength(300);
            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SafetyDocumentId).HasMaxLength(128);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    public class DocumentDesignTimeDbContextFactory : IDesignTimeDbContextFactory<SDSFinderContext>
    {
        public SDSFinderContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<SDSFinderContext>();
            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Local")
            {
                optionsBuilder.UseSqlServer("Server=localhost;Database=SDSFinder;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=false;");
            }
            else
            {
                optionsBuilder.UseSqlServer("Server=app-db-dev;Database=SDSFinder;Trusted_Connection=True;MultipleActiveResultSets=True;Encrypt=false;");
            }

            return new SDSFinderContext(optionsBuilder.Options);
        }
    }
}
