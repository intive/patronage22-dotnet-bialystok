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
        }
    }
}