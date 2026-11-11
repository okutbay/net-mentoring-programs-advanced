using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Models.Cataloging;

public class CategoryDto : BaseModel
{
    public string Name { get; set; }
    public string Image { get; set; }
    public string? ParentCategoryId { get; set; }
    public List<CategoryDto> Children { get; set; } = new();
}
