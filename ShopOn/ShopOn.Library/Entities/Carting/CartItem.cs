using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Entities.Carting;

public class CartItem : BaseMongoDbEntity
{
    public string CartId { get; set; }

    public string Name { get; set; }

    // ExtGuid of Cart Item Status
    public string StatusId { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
