using ShopOn.Library.Entities;
using System.ComponentModel.DataAnnotations;

namespace ShopOn.Catalog.Library.Entities;

public class Category : BaseSqlEntity
{
    public string Name { get; set; }
    public string Image { get; set; }
    public int? ParentCategoryId { get; set; }
    public Category? ParentCategory { get; set; }
    public ICollection<Category> Children { get; set; } = new List<Category>();
}
