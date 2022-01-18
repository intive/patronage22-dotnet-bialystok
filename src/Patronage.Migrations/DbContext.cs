using Patronage.Models;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Patronage.Migrations;
public class TableContext : DbContext
{
    public DbSet<Table> Tables { get; set; }

    public TableContext()
    {

    }
    protected override void OnModelCreating(DbModelBuilder modelBuilder)
    {
        throw new UnintentionalCodeFirstException();
    }
}
