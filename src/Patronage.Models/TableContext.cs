using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Patronage.Api;

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
        //Very important!!!
        //Set every string field to .IsUnicode(false);
        //Every string field should have maxlen defined so migrations doesn't create varchar(max) (not supported by postgre)
        //Every datetime column should be exclicitely set to datetime (not datetime2)
        //Do not use .HasColumnType("datetime"); it breaks postgre

        modelBuilder.HasDefaultSchema("public");

        #region Project

        modelBuilder.Entity<Project>()
            .Property(p => p.Alias)
            .HasMaxLength(256)
            .IsUnicode(false);

        modelBuilder.Entity<Project>()
            .Property(p => p.Name)
            .HasMaxLength(1024)
            .IsUnicode(false);

        modelBuilder.Entity<Project>()
            .Property(p => p.Description)
            .HasMaxLength(1024)
            .IsUnicode(false);

        modelBuilder.Entity<Project>()
            .Property(p => p.CreatedOn)
            .HasPrecision(0);

        modelBuilder.Entity<Project>()
            .Property(p => p.ModifiedOn)
            .HasPrecision(0);

        modelBuilder.Entity<Project>()
             .Property(r => r.ModifiedOn);

        #endregion

        #region logTable
        modelBuilder.Entity<Log>()
                .HasKey(e => e.Id);

        modelBuilder.Entity<Log>()
            .Property(r => r.MachineName)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);

        modelBuilder.Entity<Log>()
            .Property(r => r.Logged)
            .IsRequired();

        modelBuilder.Entity<Log>()
            .Property(r => r.Level)
            .IsRequired()
            .HasMaxLength(50)
            .IsUnicode(false);

        modelBuilder.Entity<Log>()
            .Property(r => r.Message)
            .IsRequired()
            .HasMaxLength(1024)
            .IsUnicode(false);

        modelBuilder.Entity<Log>()
            .Property(r => r.Logger)
            .IsRequired(false)
            .HasMaxLength(250)
            .IsUnicode(false);

        modelBuilder.Entity<Log>()
            .Property(r => r.Callsite)
            .IsRequired(false)
            .IsUnicode(false);

        modelBuilder.Entity<Log>()
            .Property(r => r.Exception)
            .IsRequired(false)
            .HasMaxLength(1024)
            .IsUnicode(false);
        #endregion

        #region Board

        modelBuilder.Entity<Board>()
            .HasKey(a => a.Id);

        modelBuilder.Entity<Board>()
            .Property(a => a.Alias)
            .HasMaxLength(256)
            .IsUnicode(false);

        modelBuilder.Entity<Board>()
            .Property(a => a.Name)
            .HasMaxLength(1024)
            .IsUnicode(false);

        modelBuilder.Entity<Board>()
            .Property(a => a.Description)
            .HasMaxLength(1024)
            .IsUnicode(false);

        modelBuilder.Entity<Board>()
            .Property(a => a.ProjectId)
            .IsRequired();

        modelBuilder.Entity<Board>()
            .Property(a => a.CreatedOn)
            .IsRequired();

        modelBuilder.Entity<Board>()
             .Property(r => r.ModifiedOn);
        #endregion

        #region Issue
        modelBuilder.Entity<Issue>()
            .Property(r => r.Alias)
            .HasMaxLength(256)
            .IsUnicode(false);

        modelBuilder.Entity<Issue>()
             .Property(r => r.Name)
             .HasMaxLength(1024)
             .IsUnicode(false);

        modelBuilder.Entity<Issue>()
             .Property(r => r.Description)
             .HasMaxLength(1024)
             .IsUnicode(false);

        modelBuilder.Entity<Issue>()
             .Property(r => r.ProjectId)
             .IsRequired();

        modelBuilder.Entity<Issue>()
             .Property(r => r.StatusId)
             .IsRequired();

        modelBuilder.Entity<Issue>()
             .Property(r => r.CreatedOn)
             .IsRequired();

        modelBuilder.Entity<Issue>()
             .Property(r => r.ModifiedOn);
        #endregion
    }


    private void Timestamps(object sender, EntityEntryEventArgs e)
    {
        if (e.Entry.Entity is ICreatable createdEntity &&
            e.Entry.State == EntityState.Added)
        {
            createdEntity.CreatedOn = DateTime.UtcNow;
        }

        else if (e.Entry.Entity is IModifable modifiedEntity &&
        e.Entry.State == EntityState.Modified)
        {
            modifiedEntity.ModifiedOn = DateTime.UtcNow;
        }
    }

}
