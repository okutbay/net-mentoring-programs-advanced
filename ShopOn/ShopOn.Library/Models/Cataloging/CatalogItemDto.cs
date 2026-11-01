using System.ComponentModel.DataAnnotations;

namespace ShopOn.Library.Models.Cataloging;

public class CatalogItemDto : BaseModel
{
    // ExtGuid of Catalog Item Status
    public string StatusId { get; set; }

    [Timestamp]
    public byte[] RowVersion { get; set; }
}
