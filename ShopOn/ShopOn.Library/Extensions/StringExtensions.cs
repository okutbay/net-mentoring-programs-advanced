using MongoDB.Bson;

namespace ShopOn.Library.Extensions;

public static class StringExtensions
{
    public static ObjectId ToObjectId(this string value)
    {
        if (ObjectId.TryParse(value, out var objectId))
            return objectId;
        throw new ArgumentException($"The value '{value}' is not a valid ObjectId.", nameof(value));
    }
}