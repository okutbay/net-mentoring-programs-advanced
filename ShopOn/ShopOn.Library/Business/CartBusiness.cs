using AutoMapper;
using MongoDB.Bson;
using ShopOn.Library.Business.Interfaces;
using ShopOn.Library.Constants;
using ShopOn.Library.Data.Interfaces;
using ShopOn.Library.Entities.Carting;
using ShopOn.Library.Exceptions;
using ShopOn.Library.Models.Carting;
using ShopOn.Library.Providers.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Business;

public class CartBusiness(IRepository<CartItem> repository,
    IGuidProvider guidProvider,
    IMapper mapper) : ICartBusiness
{
    public async Task<CartItemDto[]> GetCartItems(string cartId, CancellationToken token = default)
    {
        ValidateCartId(cartId);

        var cartItems = await repository.GetAll(
            x => x.CartId == cartId,
            token: token);

        return mapper.Map<CartItemDto[]>(cartItems);
    }

    public async Task<CartItemDto> GetCartItem(string cartId, string cartItemId, CancellationToken token = default)
    {
        ValidateCartId(cartId);

        var cartItem = await repository.Get(
            x => x.CartId == cartId && x.ExtGuid == cartItemId,
            token: token);

        if (cartItem is null)
            throw new EntityNotFoundException($"Cart item {cartItem.CartId} ({cartItem.ExtGuid}) is not found");

        return mapper.Map<CartItemDto>(cartItem);
    }

    public async Task AddCartItemWithOutCart(CartItemDto cartItemToAdd, CancellationToken token = default)
    {
        // Check if the product already exists in the cart
        var existingCartItem = await repository.Get(
            x => x.CartId == cartItemToAdd.CartId && x.ProductId == cartItemToAdd.ProductId,
            token: token);

        if (existingCartItem is null)
            throw new EntityNotFoundException($"Cart item {cartItemToAdd.Name} ({cartItemToAdd.ExtGuid}) is not found");

        if (existingCartItem != null)
        {
            // Update quantity (enforce max quantity if needed)
            int newQuantity = existingCartItem.Quantity + cartItemToAdd.Quantity;
            if (newQuantity > Common.Cart.MaxAllowedProductQuantity)
                throw new OperationNotAvailableException($"Cannot add more than {Common.Cart.MaxAllowedProductQuantity} units of a product to the cart.");

            existingCartItem.Quantity = newQuantity;
            repository.Update(existingCartItem);
        }
        else
        {
            // Add as new cart item
            CartItem dbCartItem = mapper.Map<CartItem>(cartItemToAdd);
            dbCartItem.Id = ObjectId.GenerateNewId();
            dbCartItem.ExtGuid = guidProvider.GetGuid().ToString();
            await repository.Add(dbCartItem, token);
        }

        await repository.SaveChanges(token);
    }


    public async Task RemoveCartItemWithOutCart(string cartId, string id, CancellationToken token = default)
    {
        CartItem? cartItem = await repository.Get(
            x => x.ExtGuid == id && x.CartId == cartId,
            token: token);

        if (cartItem is null)
            throw new EntityNotFoundException($"Cart item ({cartId}) is not found");

        repository.Delete(cartItem);
        await repository.SaveChanges(token);
    }


    public async Task AddCartItem(CartItemDto cartItemToAdd, CancellationToken token = default)
    {
        ValidateCartItemDto(cartItemToAdd);
        ValidateCartId(cartItemToAdd.CartId);

        // Load all items for the cart
        var cartItems = await repository.GetAll(x => x.CartId == cartItemToAdd.CartId, token: token);
        var cart = new Cart(cartItemToAdd.CartId) { Items = cartItems.ToList() };

        // Add or update item using domain logic
        cart.AddOrUpdateItem(cartItemToAdd, Common.Cart.MaxAllowedProductQuantity);

        // Persist changes
        foreach (var item in cart.Items)
        {
            if (item.Id == ObjectId.Empty)
            {
                item.Id = ObjectId.GenerateNewId();
                item.ExtGuid = guidProvider.GetGuid().ToString();
                await repository.Add(item, token);
            }
            else
            {
                repository.Update(item);
            }
        }
        await repository.SaveChanges(token);
    }

    public async Task RemoveCartItem(string cartId, string id, CancellationToken token = default)
    {
        ValidateCartId(cartId);

        var cartItems = await repository.GetAll(x => x.CartId == cartId, token: token);
        var cart = new Cart(cartId) { Items = cartItems.ToList() };

        cart.RemoveItem(id);

        // Remove from repository
        var itemToRemove = cartItems.FirstOrDefault(i => i.ExtGuid == id);
        if (itemToRemove != null)
        {
            repository.Delete(itemToRemove);
            await repository.SaveChanges(token);
        }

        throw new EntityNotFoundException($"Cart item ({cartId}) is not found");
    }

    private void ValidateCartItemDto(CartItemDto dto)
    {
        var context = new ValidationContext(dto);
        var results = new List<ValidationResult>();
        if (!Validator.TryValidateObject(dto, context, results, true))
        {
            var errors = string.Join("; ", results.Select(r => r.ErrorMessage));
            throw new ArgumentException($"Validation failed: {errors}");
        }
    }

    private void ValidateCartId(string cartId)
    {
        if (string.IsNullOrWhiteSpace(cartId))
            throw new ArgumentException("CartId must be provided and cannot be empty.", nameof(cartId));
    }
}
