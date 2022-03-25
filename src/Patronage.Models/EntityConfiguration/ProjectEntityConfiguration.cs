using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Patronage.Models.EntityConfiguration
{
    public class ProjectEntityConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder
                .Property(p => p.Alias)
                .HasMaxLength(256);

            builder
                .Property(p => p.Name)
                .HasMaxLength(1024);

            builder
                .Property(p => p.CreatedOn)
                .HasPrecision(0);

            builder
                .Property(p => p.ModifiedOn)
                .HasPrecision(0);
        }
    }
}