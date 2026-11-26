using Serilog;
using ShopOn.Library._3rdParty.AutoMapper;
using ShopOn.Library.Providers;
using ShopOn.Library.Providers.Interfaces;
using ShopOn.Catalog.Service.Extensions;
using ShopOn.Catalog.Service.Middleware;

namespace ShopOn.Catalog.Service;



public class Program
{
    public static async Task Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        Log.Information("Starting up...");

        try
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            // Start adding Custom service registrations

            // Register SqlDBContext
            builder.Services.AddSqlDbContext(builder.Configuration);

            // Register data services
            builder.Services.AddPersistanceLayer();

            // Register business services
            builder.Services.AddBusinessServices();

            // End adding Custom service registrations

            builder.Services.AddAutoMapper(cfg => { cfg.AddProfile<MappingProfile>(); });

            builder.Services.AddScoped<IGuidProvider, GuidProvider>();

            builder.Services.AddTransient<ErrorHandling>();

            var app = builder.Build();

            app.UseMiddleware<Logging>();
            app.UseMiddleware<ErrorHandling>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            // Start endpoints

            app.MapCategoriesEndpoints();
            app.MapProductsEndpoints();

            // End endpoints

            await app.RunAsync();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Unhandled exception");
        }
        finally
        {
            await Log.CloseAndFlushAsync();
        }
    }
}