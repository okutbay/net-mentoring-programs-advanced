using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Entities.Cataloging;

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
