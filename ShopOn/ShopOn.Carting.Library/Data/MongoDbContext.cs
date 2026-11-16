using Microsoft.EntityFrameworkCore;
using MongoDB.EntityFrameworkCore.Extensions;
using ShopOn.Carting.Library.Entities;
using ShopOn.Carting.Library.Models;

namespace ShopOn.Carting.Library.Data;

public class MongoDbContext : DbContext
{
    public MongoDbContext(DbContextOptions<MongoDbContext> options) : base(options)
    {
        // Disable automatic transactions
        this.Database.AutoTransactionBehavior = AutoTransactionBehavior.Never;
    }

    public DbSet<CartItemDto> CartItems { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<CartItem>()
            .ToCollection("cartitems");
    }  
}