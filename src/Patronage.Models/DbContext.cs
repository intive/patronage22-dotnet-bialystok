using Microsoft.EntityFrameworkCore;

namespace Patronage.Models;
public class TableContext : DbContext
{
    public DbSet<Table> Tables { get; set; }

    public TableContext(DbContextOptions options) : base(options)
    {

    }

}
