namespace ShopOn.Library.Settings;

/// <summary>
/// Used to map settings from appsettings.json
/// <code>
///   "SqlDbSettings": {
///         "ConnectionString": "",
///         "DatabaseName": "ShopOn"
///   }
/// </code>
/// </summary>
public class SqlDbSettings
{
    public const string Position = "SqlDbSettings";

    public string ConnectionString { get; set; }
}
