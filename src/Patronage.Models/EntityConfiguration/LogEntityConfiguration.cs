using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Patronage.Models.EntityConfiguration
{
    public class LogEntityConfiguration : IEntityTypeConfiguration<Log>
    {
        public void Configure(EntityTypeBuilder<Log> builder)
        {
            builder
            .HasKey(e => e.Id);

            builder
                .Property(r => r.MachineName)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(r => r.Logged)
                .IsRequired();

            builder
                .Property(r => r.Level)
                .IsRequired()
                .HasMaxLength(50);

            builder
                .Property(r => r.Message)
                .IsRequired();

            builder
                .Property(r => r.Logger)
                .IsRequired(false)
                .HasMaxLength(250);

            builder
                .Property(r => r.Callsite)
                .IsRequired(false)
                .IsUnicode(false);

            builder
                .Property(r => r.Exception)
                .IsRequired(false);
        }
    }
}