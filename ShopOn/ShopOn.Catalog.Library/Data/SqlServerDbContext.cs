using Microsoft.EntityFrameworkCore;
using ShopOn.Catalog.Library.Entities;

namespace ShopOn.Catalog.Library.Data;

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