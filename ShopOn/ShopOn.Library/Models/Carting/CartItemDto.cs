using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Models.Carting;

public class CartItemDto : BaseModel
{   
    public string CartId { get; set; }

    public string ProductId { get; set; }

    public string Name { get; set; }
    
    public string StatusId { get; set; }

    public string Image { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
