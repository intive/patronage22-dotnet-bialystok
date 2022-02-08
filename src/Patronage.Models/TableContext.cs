using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Patronage.Common;
using Patronage.Common.Entities;

namespace Patronage.Models;
public class TableContext : DbContext
{
    public virtual DbSet<Table> Tables { get; set; }
    public virtual DbSet<Issue> Issues { get; set; }
    public virtual DbSet<Project> Projects { get; set; }

    public TableContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.StateChanged += Timestamps;
        ChangeTracker.Tracked += Timestamps;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        

        modelBuilder.Entity<Table>()
            .HasKey(p => p.Id);

        #region Project

        modelBuilder.Entity<Project>()
            .Property(p => p.Alias)
            .HasMaxLength(256);

        modelBuilder.Entity<Project>()
            .Property(p => p.Name)
            .HasMaxLength(1024);

        modelBuilder.Entity<Project>()
            .Property(p => p.CreatedOn)
            .HasPrecision(0);

        modelBuilder.Entity<Project>()
            .Property(p => p.ModifiedOn)
            .HasPrecision(0);

        

        #endregion
    }


    private void Timestamps(object sender, EntityEntryEventArgs e)
    {
        if (e.Entry.Entity is ICreatable createdEntity &&
            e.Entry.State == EntityState.Added)
        {
            createdEntity.CreatedOn = DateTime.UtcNow;
        }

        if (e.Entry.Entity is IModifable modifiedEntity &&
        e.Entry.State == EntityState.Modified)
        {
            modifiedEntity.ModifiedOn = DateTime.UtcNow;
        }
    }

}
