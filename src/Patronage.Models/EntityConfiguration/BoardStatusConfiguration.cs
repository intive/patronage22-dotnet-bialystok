using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Patronage.Models.EntityConfiguration
{
    public class BoardStatusConfiguration : IEntityTypeConfiguration<BoardStatus>
    {
        public void Configure(EntityTypeBuilder<BoardStatus> builder)
        {
            builder
            .HasKey(bs => new { bs.BoardId, bs.StatusId });

            builder
                .HasOne(b => b.Board)
                .WithMany(s => s.BoardStatuses)
                .HasForeignKey(bi => bi.BoardId);

            builder
                .HasOne(b => b.Status)
                .WithMany(s => s.BoardStatuses)
                .HasForeignKey(si => si.StatusId);
        }
    }
}