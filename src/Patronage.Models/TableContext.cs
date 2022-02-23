﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Patronage.Api;

namespace Patronage.Models;
public class TableContext : DbContext
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

}
