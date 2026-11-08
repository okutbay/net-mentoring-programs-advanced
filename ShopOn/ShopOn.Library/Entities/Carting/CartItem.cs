using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Entities.Carting;

public class CartItem : BaseMongoDbEntity
{
    public string CartId { get; set; }

    // ExtGuid of Product
    public string ProductId { get; set; }

    public string Name { get; set; }

    public string Image { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
