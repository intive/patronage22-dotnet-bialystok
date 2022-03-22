using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Patronage.Models.EntityConfiguration
{
    public class IssueEntityConfiguration : IEntityTypeConfiguration<Issue>
    {
        public void Configure(EntityTypeBuilder<Issue> builder)
        {
            builder
                .Property(r => r.Alias)
                .HasMaxLength(256);

            builder
                 .Property(r => r.Name)
                 .HasMaxLength(1024);

            builder
                 .Property(r => r.Description)
                 .IsRequired(false);

            builder
                 .Property(r => r.ProjectId)
                 .IsRequired();

            builder
                 .Property(r => r.StatusId)
                 .IsRequired();

            builder
                 .Property(r => r.BoardId)
                 .IsRequired(false);

            builder
                 .Property(r => r.CreatedOn)
                 .IsRequired();

            builder
                 .Property(r => r.AssignUserId)
                 .IsRequired(false);

            builder
                .HasOne(p => p.User)
                .WithMany(b => b.Issues)
                .HasForeignKey(p => p.AssignUserId)
                .IsRequired(false);

            builder
                .HasOne(b => b.Board)
                .WithMany(b => b.Issues)
                .HasForeignKey(b => b.BoardId)
                .IsRequired(false);
        }
    }
}