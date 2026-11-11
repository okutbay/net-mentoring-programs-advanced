using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Entities.Cataloging;

public class Category : BaseSqlEntity
{
    public string Name { get; set; }

    public string Image { get; set; }
}
