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
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=app-db-dev.ica.com;Database=SDSFinder;Integrated Security=true;Encrypt=false");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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
