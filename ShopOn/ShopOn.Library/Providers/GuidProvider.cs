using ShopOn.Library.Providers.Interfaces;

namespace ShopOn.Library.Providers;

public class GuidProvider : IGuidProvider
{
    public Guid GetGuid()
    { 
        return Guid.NewGuid();
    }
}
