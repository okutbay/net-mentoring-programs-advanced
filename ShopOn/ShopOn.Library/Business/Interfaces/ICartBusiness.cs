using ShopOn.Library.Models.Carting;

namespace ShopOn.Library.Business.Interfaces;

public interface ICartBusiness
{
    public Task<CartItemDto[]> GetCartItems(CancellationToken token = default);
    public Task AddCartItem(CartItemDto cartItemToAdd, CancellationToken token = default);
}
