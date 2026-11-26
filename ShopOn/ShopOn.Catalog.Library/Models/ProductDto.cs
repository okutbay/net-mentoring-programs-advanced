using ShopOn.Catalog.Library.Entities;
using ShopOn.Library.Models;
using System.ComponentModel.DataAnnotations;

namespace ShopOn.Catalog.Library.Models;

public class ProductDto : BaseModel
{
    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [StringLength(500)]
    public string Description { get; set; }

    [StringLength(200)]
    public string Image { get; set; }

    [Required]
    public string CategoryId { get; set; }

    public CategoryDto Category { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be non-negative.")]
    public decimal Price { get; set; }

    [Range(1, int.MaxValue, ErrorMessage = "Amount must be positive.")]
    public int Amount { get; set; }
}
