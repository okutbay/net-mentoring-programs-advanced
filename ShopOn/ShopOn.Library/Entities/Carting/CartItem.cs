using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Entities.Carting;

public class CartItem : BaseMongoDbEntity
{
    public string CartId { get; set; }

    public string Name { get; set; }

    // ExtGuid of Cart Item Status
    public string StatusId { get; set; }

    public string Image { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }


    [Timestamp]
    public byte[] RowVersion { get; set; }
}
