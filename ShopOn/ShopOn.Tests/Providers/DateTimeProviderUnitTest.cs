using ShopOn.Library.Providers.Interfaces;

namespace ShopOn.Tests.Providers;

public class DateTimeProviderUnitTest : IDateTimeProvider
{
    private DateTime _currentDateTime;

    public DateTimeProviderUnitTest(DateTime currentDateTime)
    {
        _currentDateTime = currentDateTime;
    }

    public DateTime GetCurrentDateTime()
    {
        return _currentDateTime;
    }
}