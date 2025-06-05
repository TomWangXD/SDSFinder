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
    }

    public virtual DbSet<Document> Documents { get; set; }
    public virtual DbSet<vwGhsLanguageAttributeLookup> vwGhsLanguageAttributeLookup { get; set; }

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
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF0FFBCA7937");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<vwGhsLanguageAttributeLookup>(entity =>
        {
            entity.ToView("vw_GHS_Language_AttributeLookup");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
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
