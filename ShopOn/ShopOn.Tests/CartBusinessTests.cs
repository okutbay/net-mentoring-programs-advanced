using AutoMapper;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using Moq;
using ShopOn.Library.Business;
using ShopOn.Library.Data.Interfaces;
using ShopOn.Library.Entities.Carting;
using ShopOn.Library.Models.Carting;
using ShopOn.Library.Providers.Interfaces;
using ShopOn.Tests.Providers;

namespace ShopOn.Tests;

public class CartBusinessTests
{
    private readonly Mock<IRepository<CartItem>> _mockRepository;
    private readonly Mock<ILogger<CartItem>> _mockLogger;
    private readonly Mock<IMapper> _mockMapper;
    private readonly Mock<IMemoryCache> _mockMemoryCache;
    private readonly CartBusiness _cartBusiness;
    private readonly IGuidProvider _guidProvider;

    public CartBusinessTests()
    {
        _mockRepository = new Mock<IRepository<CartItem>>();
        _mockLogger = new Mock<ILogger<CartItem>>();
        _mockMapper = new Mock<IMapper>();
        _mockMemoryCache = new Mock<IMemoryCache>();
        _guidProvider = new GuidProviderUnitTest(Guid.NewGuid());
        _cartBusiness = new CartBusiness(_mockRepository.Object, 
            _guidProvider, 
            _mockMapper.Object);        
    }

    [Fact]
    public async Task GetCartItems_ReturnsCorrectData()
    {
        // Arrange
        var cartItems = new List<CartItem>
        {
            new CartItem { Id = ObjectId.GenerateNewId(), Name = "CartItem 1" },
            new CartItem { Id = ObjectId.GenerateNewId(), Name = "CartItem 2" }
        };
        var cartItemsDto = cartItems.Select(e => new CartItemDto { Name = e.Name }).ToArray();

        _mockRepository.Setup(x => x.GetAll(It.IsAny<CancellationToken>())).ReturnsAsync(cartItems);
        _mockMapper.Setup(x => x.Map<IEnumerable<CartItemDto>>(It.IsAny<IEnumerable<CartItem>>())).Returns(cartItemsDto);
        _mockMemoryCache
            .Setup(x => x.CreateEntry(It.IsAny<object>()))
            .Returns(Mock.Of<ICacheEntry>);

        // Act
        var result = await _cartBusiness.GetCartItems("7399284F-C4B9-4E73-ADA6-6F069221D0EE");

        // Assert
        Assert.NotNull(result);
        Assert.Equal(2, result.Length);
        Assert.Equal("CartItem 1", result[0].Name);
        Assert.Equal("CartItem 2", result[1].Name);
    }

    [Fact]
    public async Task AddCartItem_AddsCartItemSuccessfully()
    {
        // Arrange
        var cartItemDto = new CartItemDto { Name = "New CartItem" };
        var dbCartItem = new CartItem { Name = cartItemDto.Name };

        _mockMapper.Setup(x => x.Map<CartItem>(cartItemDto)).Returns(dbCartItem);
        _mockRepository.Setup(x => x.Add(It.IsAny<CartItem>(), It.IsAny<CancellationToken>())).Verifiable();
        _mockRepository.Setup(x => x.SaveChanges(It.IsAny<CancellationToken>())).Verifiable();

        // Act
        await _cartBusiness.AddCartItem(cartItemDto);

        // Assert
        _mockRepository.Verify(x => x.Add(It.Is<CartItem>(e => e.Name == "New CartItem" && e.Id != ObjectId.Empty && !string.IsNullOrWhiteSpace(e.ExtGuid)), It.IsAny<CancellationToken>()), Times.Once());
        _mockRepository.Verify(x => x.SaveChanges(It.IsAny<CancellationToken>()), Times.Once());
    }
}
