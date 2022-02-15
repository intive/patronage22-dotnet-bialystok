using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Patronage.Common;

namespace Patronage.Models;
public class TableContext : DbContext
{
    public virtual DbSet<Issue> Issues { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Log> Logs { get; set; }
    public virtual DbSet<Board> Boards { get; set; }

    public TableContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.StateChanged += Timestamps;
        ChangeTracker.Tracked += Timestamps;
    }
   
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
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

        #region Board

        modelBuilder.Entity<Board>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Board>()
            .Property(a => a.Alias)
            .HasMaxLength(256);

        modelBuilder.Entity<Board>()
            .Property(a => a.Name)
            .HasMaxLength(1024);

        modelBuilder.Entity<Board>()
            .Property(a => a.ProjectId)
            .IsRequired();

        modelBuilder.Entity<Board>()
            .Property(a => a.CreatedOn)
            .IsRequired();
        #endregion

        #region Issue
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
