using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Entities.Cataloging;

public class CatalogItem : BaseSqlEntity
{
    // ExtGuid of Cart Item Status
    public string StatusId { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
