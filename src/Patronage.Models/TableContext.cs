using Microsoft.EntityFrameworkCore;
using Patronage.Common.Entities;

namespace Patronage.Models;
public class TableContext : DbContext
{
    public virtual DbSet<Table> Tables { get; set; }
    public virtual DbSet<Issue> Issues { get; set; }

    public TableContext(DbContextOptions options) : base(options)
    {

    }

}
