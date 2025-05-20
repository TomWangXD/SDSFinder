using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Name=app");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Document>(entity =>
        {
            entity.HasKey(e => e.DocumentId).HasName("PK__Document__1ABEEF0FFBCA7937");

            entity.Property(e => e.CreatedDate).HasDefaultValueSql("(getdate())");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
