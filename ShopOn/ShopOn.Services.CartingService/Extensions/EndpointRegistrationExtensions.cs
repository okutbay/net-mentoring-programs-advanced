using ShopOn.Library.Business.Interfaces;
using ShopOn.Library.Models.Carting;

namespace ShopOn.WebAPI.Extensions;

internal static partial class EndpointRegistrationExtensions
{
    public static void MapCartItemsEndpoints(this WebApplication app)
    {
        var cartItemsGroup = app.MapGroup("cartitems").WithTags("CartItems");

        // GET /cartitems
        cartItemsGroup.MapGet("",
            async (ICartBusiness cartBusiness, ILogger<CartItemDto> logger, CancellationToken token) =>
                await cartBusiness.GetCartItems(token)
        );

        // POST New CartItem
        cartItemsGroup.MapPost("",
            async (CartItemDto cartItemDto, ICartBusiness cartItemBusiness, ILogger<CartItemDto> logger, CancellationToken token) =>
            {
                await cartItemBusiness.AddCartItem(cartItemDto, token);
                return Results.Created($"/cartitems/{cartItemDto.ExtGuid}", cartItemDto);
            }).ExcludeFromDescription();

        // DELETE /cartitems/{id} deletes a seat for a specific cart
        cartItemsGroup.MapDelete("/{cartid}/{id}",
            async (string cartid, string id, ICartBusiness cartBusiness, ILogger<CartItemDto> logger, CancellationToken token) =>
            {
                await cartBusiness.RemoveCartItem(cartid, id, token);
                return Results.Ok(id);
            });
    }
}
