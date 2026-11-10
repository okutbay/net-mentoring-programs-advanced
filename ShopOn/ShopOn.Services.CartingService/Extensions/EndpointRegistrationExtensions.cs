using ShopOn.Library.Business.Interfaces;
using ShopOn.Library.Exceptions;
using ShopOn.Library.Models.Carting;

namespace ShopOn.Services.CartingService.Extensions;

internal static partial class EndpointRegistrationExtensions
{
    public static void MapCartItemsEndpoints(this WebApplication app)
    {
        var cartItemsGroup = app.MapGroup("cartitems").WithTags("CartItems");

        // GET /cartitems/{cartid} gets items of the cart
        cartItemsGroup.MapGet("/{cartid}",
            async (string cartid, ICartBusiness cartBusiness, ILogger<CartItemDto> logger, CancellationToken token) =>
                await cartBusiness.GetCartItems(cartid, token)
        );

        // GET /cartitems/{id} gets a cart item
        cartItemsGroup.MapGet("/{cartid}/{id}",
            async (string cartid, string id, ICartBusiness cartBusiness, ILogger<CartItemDto> logger, CancellationToken token) =>
            {
                try
                {
                    var cartItem = await cartBusiness.GetCartItem(cartid, id, token);
                    return Results.Ok(cartItem);
                }
                catch (EntityNotFoundException ex)
                {

                    return Results.NotFound($"{ex.Message}");
                }
            });

        // POST New CartItem
        cartItemsGroup.MapPost("",
            async (CartItemDto cartItemDto, ICartBusiness cartItemBusiness, ILogger<CartItemDto> logger, CancellationToken token) =>
            {
                await cartItemBusiness.AddCartItem(cartItemDto, token);
                return Results.Created($"/cartitems/{cartItemDto.CartId}/{cartItemDto.ExtGuid}", cartItemDto);
            }).ExcludeFromDescription();

        // DELETE /cartitems/{id} deletes a cart item
        cartItemsGroup.MapDelete("/{cartid}/{id}",
            async (string cartid, string id, ICartBusiness cartBusiness, ILogger<CartItemDto> logger, CancellationToken token) =>
            {
                try
                {
                    await cartBusiness.RemoveCartItem(cartid, id, token);
                    return Results.Ok(id);
                }
                catch (EntityNotFoundException ex)
                {

                    return Results.NotFound($"{ex.Message}");
                }
            });
    }
}
