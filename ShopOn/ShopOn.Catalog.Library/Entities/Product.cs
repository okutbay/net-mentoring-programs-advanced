using ShopOn.Library.Entities;
using System.ComponentModel.DataAnnotations;

namespace ShopOn.Catalog.Library.Entities;

public class Product : BaseSqlEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    [Required]
    public int CategoryId { get; set; }
    public Category Category { get; set; }

    public decimal Price { get; set; }

    public int Amount { get; set; }
}
