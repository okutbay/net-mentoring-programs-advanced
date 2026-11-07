using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using MongoDB.Driver;
using ShopOn.IntegrationTests.Helpers;
using ShopOn.Library.Entities.Carting;
using ShopOn.Library.Models.Carting;
using System.Text;
using System.Text.Json;

namespace ShopOn.IntegrationTests;

public class CartApiTests : IClassFixture<MongoDbFixture>, IDisposable
{
    private readonly MongoDbFixture _fixture;
    private readonly HttpClient _client;
    private readonly CartItemDto _testCartItem;

    public CartApiTests(MongoDbFixture fixture)
    {
        Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "Test");

        _fixture = fixture;
        var appFactory = new WebApplicationFactory<ShopOn.Services.CartingService.Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.UseEnvironment("Test");
                builder.ConfigureTestServices(services =>
                {
                    services.RemoveAll<IMongoClient>();
                    services.AddSingleton<IMongoClient>((_) => _fixture.Client);
                });
            });
        _client = appFactory.CreateClient();

        _testCartItem = new CartItemDto
        {
            Name = "Test CartItem",
            ExtGuid = "9D3B400D-3490-4437-8AC3-93CE9E61695A",
        };
    }

    [Fact]
    public async Task GetAllCartItems_ShouldReturnCartItems()
    {
        // Arrange
        var mongoDb = _fixture.Client.GetDatabase("ShopOnTest");
        var collection = mongoDb.GetCollection<CartItemDto>("cartitems");
        await collection.InsertOneAsync(_testCartItem);

        // Act
        var response = await _client.GetAsync("/cartitems");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var items = JsonSerializer.Deserialize<ICollection<CartItemDto>>(content, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        // Assert
        Assert.NotNull(items);
        Assert.NotEmpty(items);
        Assert.Equal(1, items.Count);
        var resultItem = items.FirstOrDefault();
        Assert.NotNull(resultItem);
    }

    [Fact]
    public async Task GetAllCartItems_NoCartItemsInDb_ShouldReturnNoCartItems()
    {
        // Arrange

        // Act
        var response = await _client.GetAsync("/cartitems");
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();
        var items = JsonSerializer.Deserialize<ICollection<CartItemDto>>(content, new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        });

        // Assert
        Assert.NotNull(items);
        Assert.Empty(items);
    }

    [Fact]
    public async Task AddCartItem_ShouldAddNewCartItem()
    {
        // Arrange
        string jsonPayload = JsonSerializer.Serialize(_testCartItem, typeof(CartItemDto));
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");

        // Act
        var response = await _client.PostAsync($"/cartitems", content);
        response.EnsureSuccessStatusCode();

        // Assert
        var mongoDb = _fixture.Client.GetDatabase("ShopOnTest");
        var collection = mongoDb.GetCollection<CartItem>("cartitems");
        var item = await collection.Find(x => x.Name == _testCartItem.Name).FirstOrDefaultAsync();

        Assert.NotNull(item);
        Assert.Equal(_testCartItem.Name, item.Name);
        Assert.Equal(_testCartItem.CartId, item.CartId);
    }

    public void Dispose()
    {
        var mongoDb = _fixture.Client.GetDatabase("ShopOnTest");
        var collection = mongoDb.GetCollection<CartItemDto>("cartitems");
        collection.DeleteManyAsync(_ => true).Wait();
    }
}
