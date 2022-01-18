using Microsoft.EntityFrameworkCore;

namespace Patronage.Models;
public class TableContext : DbContext
{
    public DbSet<Table> Tables { get; set; }

    public TableContext(DbContextOptions<TableContext> options)
            : base(options)
    {

    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        throw new NotImplementedException();
    }
}
