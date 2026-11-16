using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopOn.Carting.Library.Business;
using ShopOn.Carting.Library.Business.Interfaces;
using ShopOn.Carting.Library.Data;
using ShopOn.Carting.Library.Entities;
using ShopOn.Library.Data;
using ShopOn.Library.Data.Interfaces;
using ShopOn.Library.Settings;

namespace ShopOn.Carting.Service.Extensions;

internal static class RegistrationExtensions
{
    public static IServiceCollection AddMongoDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.Configure<MongoDbSettings>(configuration.GetSection(MongoDbSettings.Position));
        services.AddDbContext<MongoDbContext>((sp, options) =>
        {
            // MongoDB
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            options.UseMongoDB(settings.ConnectionString, settings.DatabaseName);
        });
        return services;
    }

    public static IServiceCollection AddPersistanceLayer(this IServiceCollection services)
    {
        //MongoDbContext
        services.AddScoped<IRepository<CartItem>, Repository<CartItem, MongoDbContext>>();

        return services;
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ICartBusiness, CartBusiness>();

        return services;
    }
}
