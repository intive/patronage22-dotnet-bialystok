using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Patronage.Api;

namespace Patronage.Models;
public class TableContext : IdentityDbContext<
        ApplicationUser, IdentityRole, string,
        IdentityUserClaim<string>,
        IdentityUserRole<string>,
        IdentityUserLogin<string>,
        IdentityRoleClaim<string>,      
        TokenUser>
{
    public virtual DbSet<Issue> Issues => Set<Issue>();
    public virtual DbSet<Project> Projects => Set<Project>();
    public virtual DbSet<Log> Logs => Set<Log>();
    public virtual DbSet<Board> Boards => Set<Board>();
    public virtual DbSet<Status> Statuses => Set<Status>();
    public virtual DbSet<BoardStatus> BoardsStatus => Set<BoardStatus>();

    public TableContext(DbContextOptions options) : base(options)
    {
        ChangeTracker.StateChanged += Timestamps;
        ChangeTracker.Tracked += Timestamps;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // I had to add it to fix problems with identity
        base.OnModelCreating(modelBuilder);
        //Very important!!!
        //Set every string field to .IsUnicode(false);
        //Do not use .HasColumnType("datetime"); it breaks postgre

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
            .IsRequired(false)
            .IsUnicode(false);

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

        #region DataSeed

        TestDataSeed(modelBuilder);

        #endregion
    }

    private void Timestamps(object? sender, EntityEntryEventArgs e)
    {
        if(sender is null)
        {
            return;
        }
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

    private void TestDataSeed(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Project>().HasData(
        new Project()
        {
            Id = 1,
            Name = "First project",
            Alias = "1st",
            Description = "This is a description of first test project",
            IsActive = true
        },

        new Project()
        {
            Id = 2,
            Name = "Second test project",
            Alias = "2nd",
            Description = "This is a description of 2nd test project",
            IsActive = false
        },

        new Project()
        {
            Id = 3,
            Name = "Third test project",
            Alias = "3rd",
            Description = null,
            IsActive = false
        });

        modelBuilder.Entity<Board>().HasData(
                new Board()
                {
                    Id = 1,
                    Name = "First test Board",
                    Alias = "1st board",
                    Description = "This is a description of first test board",
                    ProjectId = 1,
                    IsActive = true
                },

                new Board()
                {
                    Id = 2,
                    Name = "Second test Board",
                    Alias = "2nd board",
                    Description = "This is a description of second test board",
                    ProjectId = 1,
                    IsActive = false
                });

        modelBuilder.Entity<Issue>().HasData(
                new Issue
                {
                    Id = 1,
                    Name = "First test Issue",
                    Alias = "1st issue",
                    Description = "This is a description of first test issue. This Issue is connected to a Board",
                    ProjectId = 1,
                    StatusId = 1,
                    BoardId = 1,
                    IsActive = true
                },

                new Issue
                {
                    Id = 2,
                    Name = "Second test Issue",
                    Alias = "2nd issue",
                    Description = "This is a description of second test issue. This Issue isn't connected to a Board",
                    ProjectId = 1,
                    StatusId = 1,
                    BoardId = null,
                    IsActive = true
                });

        modelBuilder.Entity<Status>().HasData(
                new Status
                {
                    Id = 1,
                    Code = "TO DO"
                },

                new Status
                {
                    Id = 2,
                    Code = "IN PROGRESS"
                },

                new Status
                {
                    Id = 3,
                    Code = "DONE"
                });

        modelBuilder.Entity<BoardStatus>().HasData(
                new BoardStatus
                {
                    BoardId = 1,
                    StatusId = 1
                },

                new BoardStatus
                {
                    BoardId = 1,
                    StatusId = 2
                },

                new BoardStatus
                {
                    BoardId = 2,
                    StatusId = 3
                });

        modelBuilder.Entity<ApplicationUser>().HasData(
            new ApplicationUser
            {
                Id = "679381f2-06a1-4e22-beda-179e8e9e3236",
                UserName = "TestUser1",
                Email = "test1@mail.com",
                PasswordHash = "AQAAAAEAACcQAAAAEIR44hzbnj/pCIqsHG4vIPm/ARO5F+qPlxQp9Wjhn+EBi/q73B+RlmXZNV+yUOvgPQ=="
            });
    }
}
