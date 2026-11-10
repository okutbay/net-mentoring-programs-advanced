using ShopOn.Library.Providers.Interfaces;

namespace ShopOn.IntegrationTests.Providers;

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