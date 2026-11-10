using Serilog;
using ShopOn.Library._3rdParty.AutoMapper;
using ShopOn.Library.Providers;
using ShopOn.Library.Providers.Interfaces;
using ShopOn.Services.CartingService.Extensions;
using ShopOn.Services.CartingService.Middleware;

namespace ShopOn.Services.CartingService;

public class Program
{
    public static void Main(string[] args)
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

            // Register MongoDBContext
            builder.Services.AddMongoDbContext(builder.Configuration);

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

            app.MapCartItemsEndpoints();

            // End endpoints

            app.Run();
        }
        catch (Exception ex)
        {
            Log.Fatal(ex, "Unhandled exception");
        }
        finally
        {
            Log.CloseAndFlushAsync();
        }
    }
}
