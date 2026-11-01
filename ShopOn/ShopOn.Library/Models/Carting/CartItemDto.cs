using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Models.Carting;

public class CartItemDto : BaseModel
{   
    // ExtGuid of Cart Item Status

    public string CartId { get; set; }

    public string Name { get; set; }
    
    public string StatusId { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
