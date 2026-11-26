using ShopOn.Library.Entities;
using System.ComponentModel.DataAnnotations;

namespace ShopOn.Carting.Library.Entities;

public class CartItem : BaseMongoDbEntity
{
    public string CartId { get; set; }

    // ExtGuid of Product
    public int ProductId { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }
}
