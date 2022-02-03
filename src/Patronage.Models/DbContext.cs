using Microsoft.EntityFrameworkCore;

namespace Patronage.Models
{
    public class TableContext : DbContext
    {
        public  DbSet<Table> Tables { get; set; }
        public  DbSet<Log> Logs { get; set; }

        public TableContext(DbContextOptions options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
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
                .IsRequired(false);
                        
            modelBuilder.Entity<Log>()
                .Property(r => r.Exception)
                .IsRequired(false);

        }
    
    
    }
}
