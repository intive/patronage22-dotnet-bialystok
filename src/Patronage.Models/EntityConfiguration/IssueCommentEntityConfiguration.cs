using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patronage.Models.EntityConfiguration
{
    public class IssueCommentEntityConfiguration : IEntityTypeConfiguration<Comment>
    {
        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder
                .Property(r => r.Content)
                .HasMaxLength(500);

            builder
                .HasOne(p => p.User)
                .WithMany(b => b.Comment)
                .HasForeignKey(p => p.ApplicationUserId);

            builder
                .HasOne(p => p.Issue)
                .WithMany(b => b.Comment)
                .HasForeignKey(p => p.ApplicationUserId);

        }
    }
}
