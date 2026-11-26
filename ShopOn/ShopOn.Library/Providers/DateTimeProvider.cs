using ShopOn.Library.Providers.Interfaces;

namespace ShopOn.Library.Providers;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime GetCurrentDateTime()
    {
        return DateTime.Now;
    }
}