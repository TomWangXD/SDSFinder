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

    public virtual DbSet<Document> Documents { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_DocumentNew");

            entity.ToTable("Document");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.FileLocation).HasMaxLength(300);
            entity.Property(e => e.FileName).HasMaxLength(100);
            entity.Property(e => e.IsDeleted)
                .HasMaxLength(10)
                .IsFixedLength();
            entity.Property(e => e.ModifiedDate).HasColumnType("datetime");
            entity.Property(e => e.SafetyDocumentId).HasMaxLength(128);
        });

        OnModelCreatingPartial(modelBuilder);
    }
        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

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
