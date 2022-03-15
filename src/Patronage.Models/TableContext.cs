using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Patronage.Api;

namespace Patronage.Models;

public class TableContext : IdentityDbContext<ApplicationUser>
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
        //Do not use .HasColumnType("datetime"); it breaks postgre

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(TableContext).Assembly);

        TestDataSeed(modelBuilder);
    }

    private void Timestamps(object? sender, EntityEntryEventArgs e)
    {
        if (sender is null)
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
                    AssignUserId = "1",
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
                    AssignUserId = null,
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
                Id = "1",
                FirstName = "FirstTestFirstname",
                SecondName = "FirstTestSurname",
            });

        var ide = Guid.NewGuid().ToString();

        modelBuilder.Entity<IdentityRole>().HasData(

             new IdentityRole() { Id = ide, Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" }
             );

        modelBuilder.Entity<IdentityUserRole<string>>().HasData(
             new IdentityUserRole<string>() { RoleId = ide, UserId = "1" }
             );
    }
}