using System;

namespace ChessMath.Shared.Common
{
    public interface ITimeProvider
    {
        DateTime UtcNow { get; }
        DateTime Now { get; }
        
        TimeSpan TimeSinceStartup { get; }
    }

    public static class TimeProviderExtensions
    {
        public static long GetUnixTimeInMilliseconds(this ITimeProvider timeProvider) =>
            new DateTimeOffset(timeProvider.UtcNow)
                .ToUnixTimeMilliseconds();
    }
}
