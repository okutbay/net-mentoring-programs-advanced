using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Models.Cataloging;

public class ProductDto : BaseModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public string Category { get; set; }

    [Range(0, double.MaxValue)]
    public decimal Price { get; set; }

    public int Amount { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
