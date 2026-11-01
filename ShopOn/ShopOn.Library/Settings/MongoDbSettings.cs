namespace ShopOn.Library.Settings;

/// <summary>
/// Used to map settings from appsettings.json
/// <code>
///   "MongoDbSettings": {
///         "ConnectionString": "mongodb://localhost:27017/",
///         "DatabaseName": "ShopOn"
///   }
/// </code>
/// </summary>
public class MongoDbSettings
{
    public const string Position = "MongoDbSettings";

    public string ConnectionString { get; set; }
    public string DatabaseName { get; set; }
}
