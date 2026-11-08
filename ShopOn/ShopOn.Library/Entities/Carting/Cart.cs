using MongoDB.Bson;
using ShopOn.Library.Exceptions;
using ShopOn.Library.Models.Carting;

namespace ShopOn.Library.Entities.Carting;
public class Cart
{
    public string CartId { get; set; }
    public List<CartItem> Items { get; set; } = new();

    public Cart(string cartId)
    {
        CartId = cartId;
    }

    public CartItem? FindItem(string productId) =>
        Items.FirstOrDefault(i => i.ProductId == productId);

    public void AddOrUpdateItem(CartItemDto dto, int maxQuantity)
    {
        var item = FindItem(dto.ProductId);
        if (item != null)
        {
            int newQuantity = item.Quantity + dto.Quantity;
            if (newQuantity > maxQuantity)
                throw new OperationNotAvailableException($"Cannot add more than {maxQuantity} units of a product to the cart.");
            item.Quantity = newQuantity;
        }
        else
        {
            var newItem = new CartItem
            {
                Id = ObjectId.GenerateNewId(),
                ExtGuid = dto.ExtGuid,
                CartId = CartId,
                ProductId = dto.ProductId,
                Name = dto.Name,
                Image = dto.Image,
                Price = dto.Price,
                Quantity = dto.Quantity,
                RowVersion = dto.RowVersion
            };
            Items.Add(newItem);
        }
    }

    public void RemoveItem(string extGuid)
    {
        var item = Items.FirstOrDefault(i => i.ExtGuid == extGuid);
        if (item == null)
            throw new EntityNotFoundException($"Cart item ({extGuid}) is not found");
        Items.Remove(item);
    }
}
