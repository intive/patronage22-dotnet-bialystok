using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Patronage.Models.EntityConfiguration
{
    public class IssueCommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                 .Property(r => r.IssueId)
                 .IsRequired();

            builder
                 .Property(r => r.Content)
                 .HasMaxLength(500);

            builder
                 .Property(r => r.CreatedOn)
                 .IsRequired();

            builder
                .HasOne(p => p.Issue)
                .WithMany(b => b.Comment)
                .HasForeignKey(p => p.IssueId);

            builder
                .HasOne(p => p.User)
                .WithMany(b => b.Comment)
                .HasForeignKey(p => p.ApplicationUserId);
        }
    }
}