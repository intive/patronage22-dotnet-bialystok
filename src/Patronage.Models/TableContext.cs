using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Patronage.Common;

namespace Patronage.Models;
public class TableContext : DbContext
{
    public virtual DbSet<Table> Tables { get; set; }
    public virtual DbSet<Issue> Issues { get; set; }
    public virtual DbSet<Project> Projects { get; set; }
    public virtual DbSet<Log> Logs { get; set; }
    public virtual DbSet<Board> Boards { get; set; }
    public virtual DbSet<Status> Statuses { get; set; }
    public virtual DbSet<BoardStatus> BoardsStatus { get; set; }

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

        #region Status
        modelBuilder.Entity<Status>()
            .Property(s => s.Code)
            .IsRequired();
        #endregion

        #region BoardStatus

        modelBuilder.Entity<BoardStatus>()
            .HasKey(bs => new { bs.BoardId, bs.StatusId });


        modelBuilder.Entity<BoardStatus>()
            .HasOne(b => b.Board)
            .WithMany(s => s.BoardStatuses)
            .HasForeignKey(bi => bi.BoardId);

        modelBuilder.Entity<BoardStatus>()
            .HasOne(b => b.Status)
            .WithMany(s => s.BoardStatuses)
            .HasForeignKey(si => si.StatusId);

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
}
