using Microsoft.EntityFrameworkCore;
using Patronage.Common.Entities;

namespace Patronage.Models;
public class TableContext : DbContext
{
    public virtual DbSet<Table> Tables { get; set; }
    public virtual DbSet<Issue> Issues { get; set; }
    public virtual DbSet<Project> Projects { get; set; }

    public TableContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        #region Project

        modelBuilder.Entity<Table>()
            .HasKey(p => p.Id);

        modelBuilder.Entity<Project>()
            .Property(p => p.Alias)
            .HasMaxLength(256);

        modelBuilder.Entity<Project>()
            .Property(p => p.Name)
            .HasMaxLength(1024);

        modelBuilder.Entity<Project>()
            .Property(p => p.CreatedOn)
            .IsRequired();

        #endregion
    }
}
