using NetworkUsageTracker;
using NetworkUsageTracker.Interfaces;

namespace NetworkUsageTracker_Tests
{
    internal class MockUsageCollector : IUsageCollector
    {
        private static long bytesSent = 0;
        private static long bytesReceived = 0;

        public void MockSendBytes(long byteCount) => bytesSent += byteCount;
        public void MockReceiveBytes(long byteCount) => bytesReceived += byteCount;

        public UsageInfo GetUsageInfo()
            => new UsageInfo(bytesSent, bytesReceived);
    }
}
