using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopOn.Library.Business;
using ShopOn.Library.Business.Interfaces;
using ShopOn.Library.Data;
using ShopOn.Library.Data.Interfaces;
using ShopOn.Library.Entities.Carting;
using ShopOn.Library.Entities.Cataloging;
using ShopOn.Library.Settings;

namespace ShopOn.Services.CartingService.Extensions;

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

    public static IServiceCollection AddSqlDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.Configure<SqlDbSettings>(configuration.GetSection(SqlDbSettings.Position));

        services.AddDbContext<SqlServerDbContext>((sp, options) =>
        {
            // SqlServer
            var settings = sp.GetRequiredService<IOptions<SqlDbSettings>>().Value;
            options.UseSqlServer(settings.ConnectionString);
        });

        return services;
    }

    public static IServiceCollection AddPersistanceLayer(this IServiceCollection services)
    {
        //MongoDbContext
        services.AddScoped<IRepository<CartItem>, Repository<CartItem, MongoDbContext>>();

        //SqlServerDbContext
        services.AddScoped<IRepository<CatalogItem>, Repository<CatalogItem, SqlServerDbContext>>();

        return services;
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ICartBusiness, CartBusiness>();
        services.AddScoped<ICatalogBusiness, CatalogBusiness>();

        return services;
    }
}
