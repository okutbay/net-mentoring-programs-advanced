//using Castle.DynamicProxy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ShopOn.Catalog.Library.Business;
using ShopOn.Catalog.Library.Business.Interfaces;
using ShopOn.Catalog.Library.Data;
using ShopOn.Catalog.Library.Entities;
using ShopOn.Library.Data;
using ShopOn.Library.Data.Interfaces;
using ShopOn.Library.Settings;

namespace ShopOn.Catalog.Service.Extensions;

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
