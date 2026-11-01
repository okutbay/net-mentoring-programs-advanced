using AutoMapper;
using MongoDB.Bson;
using ShopOn.Library.Business.Interfaces;
using ShopOn.Library.Data.Interfaces;
using ShopOn.Library.Entities.Carting;
using ShopOn.Library.Models.Carting;
using ShopOn.Library.Providers.Interfaces;

namespace ShopOn.Library.Business;

public class CartBusiness(IRepository<CartItem> repository,
    IGuidProvider guidProvider,
    IMapper mapper) : ICartBusiness
{
    public async Task<CartItemDto[]> GetCartItems(CancellationToken token = default)
    {
        var cartItems = await repository.GetAll(token);
        IEnumerable<CartItemDto> cartItemsDto = mapper.Map<IEnumerable<CartItemDto>>(cartItems);
        CartItemDto[] cartItemsDtoArray = cartItemsDto.ToArray();

        return cartItemsDtoArray;
    }

    public async Task AddCartItem(CartItemDto cartItemToAdd, CancellationToken token = default)
    {
        CartItem dbCartItem = mapper.Map<CartItem>(cartItemToAdd);
        dbCartItem.Id = ObjectId.GenerateNewId();
        dbCartItem.ExtGuid = guidProvider.GetGuid().ToString();
        await repository.Add(dbCartItem, token);
        await repository.SaveChanges(token);
    }
}
