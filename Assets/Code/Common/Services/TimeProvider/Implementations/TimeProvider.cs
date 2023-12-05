using System;
using System.Diagnostics;

namespace ChessMath.Shared.Common.TimeProviders
{
    public class TimeProvider : ITimeProvider
    {
        private readonly Stopwatch stopwatch;

        public TimeProvider()
        {
            stopwatch = Stopwatch.StartNew();
        }
        
        public TimeSpan TimeSinceStartup => stopwatch.Elapsed;
        public DateTime UtcNow => DateTime.UtcNow;
        public DateTime Now => DateTime.Now;
    }
}
