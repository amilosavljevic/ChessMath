using System;

namespace ChessMath.Shared.Common.TimeProviders
{
    public class ManualTimeProvider : ITimeProvider
    {
        public TimeSpan TimeSinceStartup { get; set; }
        public DateTime UtcNow { get; set; }
        public DateTime Now { get; set; }

        public ManualTimeProvider() { }

        public ManualTimeProvider(DateTime now) =>
            SetTime(now);

        public void SetTime(DateTime dateTime)
        {
            UtcNow = dateTime;
            Now = dateTime;
            TimeSinceStartup = TimeSpan.Zero;
        }

        public void FastForward(TimeSpan span)
        {
            UtcNow += span;
            Now += span;
            TimeSinceStartup += span;
        }
    }
}
