using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Models.Carting;

public class CartItemDto : BaseModel
{
    [Required]
    public string CartId { get; set; }

    [Required]
    public string ProductId { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public string StatusId { get; set; }
    public string Image { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    [Range(1, 99)]
    public int Quantity { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
