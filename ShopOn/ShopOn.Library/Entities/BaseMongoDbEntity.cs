using MongoDB.Bson;

namespace ShopOn.Library.Entities;

public class BaseMongoDbEntity : BaseEntity
{
    public ObjectId Id { get; set; }
}
