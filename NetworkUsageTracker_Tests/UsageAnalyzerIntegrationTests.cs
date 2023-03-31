using NetworkUsageTracker;

namespace NetworkUsageTracker_Tests
{
    public class UsageAnalyzerIntegrationTests
    {
        [Fact]
        public void TestUsageAnalyzer()
        {
            var usageCollector = new MockUsageCollector();
            var usageListener = new UsageListener();
            var usageAnalyzer = new UsageAnalyzer(usageCollector, usageListener);

            var averageUsageInfo = usageAnalyzer.GetRelativeUsageInfo();
            Assert.Equal(0, averageUsageInfo.BytesReceived);
            Assert.Equal(0, averageUsageInfo.BytesSent);

            usageCollector.MockSendBytes(1);
            usageCollector.MockReceiveBytes(1);

            var usageInfo = usageAnalyzer.GetRelativeUsageInfo();
            Assert.Equal(1, usageInfo.BytesReceived);
            Assert.Equal(1, usageInfo.BytesSent);

            usageCollector.MockSendBytes(3);
            usageCollector.MockReceiveBytes(3);

            usageInfo = usageAnalyzer.GetRelativeUsageInfo();
            Assert.Equal(3, usageInfo.BytesReceived);
            Assert.Equal(3, usageInfo.BytesSent);

            averageUsageInfo = usageAnalyzer.GetAverageUsageInfo();
            Assert.Equal(2, averageUsageInfo.BytesReceived);
            Assert.Equal(2, averageUsageInfo.BytesSent);
        }
    }
}