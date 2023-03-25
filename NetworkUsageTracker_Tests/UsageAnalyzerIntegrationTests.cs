using NetworkUsageTracker;

namespace NetworkUsageTracker_Tests
{
    public class UsageAnalyzerIntegrationTests
    {
        [Fact]
        public void TestUsageAnalyzer()
        {
            var usageCollector = new MockUsageCollector();
            var usageAnalyzer = new UsageAnalyzer(usageCollector);

            var firstUsageInfo = usageAnalyzer.GetRelativeUsageInfo();
            Assert.Equal(0, firstUsageInfo.BytesReceived);
            Assert.Equal(0, firstUsageInfo.BytesSent);

            usageCollector.MockSendBytes(100);
            usageCollector.MockReceiveBytes(100);

            var secondUsageInfo = usageAnalyzer.GetRelativeUsageInfo();
            Assert.Equal(100, secondUsageInfo.BytesReceived);
            Assert.Equal(100, secondUsageInfo.BytesSent);
        }
    }
}