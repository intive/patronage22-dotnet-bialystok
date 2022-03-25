using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Patronage.Models.EntityConfiguration
{
    public class BoardEntityConfiguration : IEntityTypeConfiguration<Board>
    {
        public void Configure(EntityTypeBuilder<Board> builder)
        {
            builder
                .HasKey(a => a.Id);

            builder
                .Property(a => a.Alias)
                .HasMaxLength(256);

            builder
                .Property(a => a.Name)
                .HasMaxLength(1024);

            builder
                .Property(a => a.ProjectId)
                .IsRequired();

            builder
                .Property(a => a.CreatedOn)
                .IsRequired();
        }
    }
}