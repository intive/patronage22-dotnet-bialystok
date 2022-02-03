using Microsoft.EntityFrameworkCore;
using Patronage.Common.Entities;

namespace Patronage.Models;
public class TableContext : DbContext
{
    public virtual DbSet<Table> Tables { get; set; }
    public virtual DbSet<Issue> Issues { get; set; }

    public TableContext(DbContextOptions options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Issue>()
            .Property(r => r.Alias)
            .HasMaxLength(256);
        modelBuilder.Entity<Issue>()
             .Property(r => r.Name)
             .HasMaxLength(1024);
        modelBuilder.Entity<Issue>()
             .Property(r => r.ProjectId)
             .IsRequired();
        modelBuilder.Entity<Issue>()
             .Property(r => r.StatusId)
             .IsRequired();
        modelBuilder.Entity<Issue>()
             .Property(r => r.CreatedOn)
             .IsRequired();
    }
}
