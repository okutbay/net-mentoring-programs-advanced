using ShopOn.Library.Providers.Interfaces;

namespace ShopOn.Tests.Providers;

public class GuidProviderUnitTest : IGuidProvider
{
    private Guid _guid;

    public GuidProviderUnitTest(Guid guid)
    {
        _guid = guid;
    }

    public Guid GetGuid()
    {
        return _guid;
    }
}