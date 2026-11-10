using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Models.Cataloging;

public class CategoryDto : BaseModel
{
    public string Name { get; set; }

    public string Image { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
