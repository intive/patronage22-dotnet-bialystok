using Microsoft.EntityFrameworkCore;
using Patronage.Common.Entities;

namespace Patronage.Models;
public class TableContext : DbContext
{
    public virtual DbSet<Table> Tables { get; set; }
    public virtual DbSet<Issue> Issues { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Log> Logs { get; set; }

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

        #region logTable
        modelBuilder.Entity<Log>()
                .HasKey(e => e.Id);

        modelBuilder.Entity<Log>()
            .Property(r => r.MachineName)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Log>()
            .Property(r => r.Logged)
            .IsRequired();

        modelBuilder.Entity<Log>()
            .Property(r => r.Level)
            .IsRequired()
            .HasMaxLength(50);

        modelBuilder.Entity<Log>()
            .Property(r => r.Message)
            .IsRequired();

        modelBuilder.Entity<Log>()
            .Property(r => r.Logger)
            .IsRequired(false)
            .HasMaxLength(250);

        modelBuilder.Entity<Log>()
            .Property(r => r.Callsite)
            .IsRequired(false);

        modelBuilder.Entity<Log>()
            .Property(r => r.Exception)
            .IsRequired(false);
        #endregion 
    }
}
