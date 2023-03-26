using NetworkUsageTracker.Interfaces;

namespace NetworkUsageTracker
{
    internal class UsageAnalyzer: IUsageAnalyzer
    {
        private long lastBytesSent = 0, lastBytesReceived = 0;

        private List<long> pastBytesSent = new List<long>();
        private List<long> pastBytesReceived = new List<long>();

        private IUsageCollector _usageCollector;

        public UsageAnalyzer(IUsageCollector usageCollector)
        {
            _usageCollector = usageCollector;
            var usageInfo = _usageCollector.GetUsageInfo();
            lastBytesSent = usageInfo.BytesSent;
            lastBytesReceived = usageInfo.BytesReceived;
        }

        public UsageInfo GetRelativeUsageInfo()
        {
            var usageInfo = _usageCollector.GetUsageInfo();

            var newBytesSent = usageInfo.BytesSent - lastBytesSent;
            var newBytesReceived = usageInfo.BytesReceived - lastBytesReceived;

            if(newBytesSent > 0)
                pastBytesSent.Add(newBytesSent);
            if (newBytesReceived > 0)
                pastBytesReceived.Add(newBytesReceived);

            lastBytesSent = usageInfo.BytesSent;
            lastBytesReceived = usageInfo.BytesReceived;

            return new UsageInfo(newBytesSent, newBytesReceived);
        }

        public UsageInfo GetAverageUsageInfo() => new UsageInfo(
            pastBytesSent.Count() > 0 ? (long)pastBytesSent.Average() : 0,
            pastBytesReceived.Count() > 0? (long)pastBytesReceived.Average() : 0
        );
    }
}
