using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using ShopOn.Library.Entities.Carting;
using ShopOn.Library.Models.Carting;

namespace ShopOn.Library.Data;

public class MongoDbContext : DbContext
{
    public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
    {

    }

    public DbSet<CartItemDto> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CartItem>()
            .ToCollection("cartitems");
    }  
}