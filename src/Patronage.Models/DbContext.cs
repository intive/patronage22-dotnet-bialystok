using Microsoft.EntityFrameworkCore;

namespace Patronage.Models;
public class TableContext : DbContext
{
    public virtual DbSet<Table> Tables { get; set; }

    public TableContext(DbContextOptions options) : base(options)
    {

    }

}
