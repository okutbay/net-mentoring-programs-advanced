using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Entities.Cataloging;

public class Product : BaseSqlEntity
{
    public string Name { get; set; }

    public string Description { get; set; }

    public string Image { get; set; }

    public string Category { get; set; }

    public string Price { get; set; }

    public string Amount { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
