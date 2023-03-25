using NetworkUsageTracker.Interfaces;

namespace NetworkUsageTracker
{
    internal class UsageAnalyzer : IUsageAnalyzer
    {
        private long lastBytesSent = 0;
        private long lastBytesReceived = 0;

        IUsageCollector _usageCollector;

        public UsageAnalyzer(IUsageCollector usageCollector)
        {
            _usageCollector = usageCollector;
        }

        public UsageInfo GetRelativeUsageInfo()
        {
            var usageInfo = _usageCollector.GetUsageInfo();

            lastBytesSent = usageInfo.BytesSent - lastBytesSent;
            lastBytesReceived = usageInfo.BytesReceived - lastBytesReceived;

            return new UsageInfo(lastBytesSent, lastBytesReceived);
        }
    }
}
