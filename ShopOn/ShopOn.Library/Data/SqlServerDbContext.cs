using Microsoft.EntityFrameworkCore;
using ShopOn.Library.Entities.Cataloging;

namespace ShopOn.Library.Data;

public class SqlServerDbContext : DbContext
{
    public SqlServerDbContext(DbContextOptions<SqlServerDbContext> options) : base(options)
    {

    }

    public DbSet<Category> Category { get; set; }
    public DbSet<Product> Product { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}