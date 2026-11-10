//using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopOn.Library.Business;
using ShopOn.Library.Business.Interfaces;
using ShopOn.Library.Data;
using ShopOn.Library.Data.Interfaces;
using ShopOn.Library.Entities.Cataloging;
using ShopOn.Library.Settings;

namespace ShopOn.Services.CatalogService.Extensions;

internal static class RegistrationExtensions
{
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
        //SqlServerDbContext
        services.AddScoped<IRepository<Category>, Repository<Category, SqlServerDbContext>>();
        services.AddScoped<IRepository<Product>, Repository<Product, SqlServerDbContext>>();

        return services;
    }

    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<ICatalogBusiness, CatalogBusiness>();

        return services;
    }
}
