using NetworkUsageTracker;
using NetworkUsageTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUsageTracker_Tests
{
    internal class MockUsageCollector : IUsageCollector
    {
        private long bytesSent = 0;
        private long bytesReceived = 0;

        public void MockSendBytes(long byteCount) => bytesSent += byteCount;
        public void MockReceiveBytes(long byteCount) => bytesReceived += byteCount;

        public UsageInfo GetUsageInfo()
            => new UsageInfo(bytesSent, bytesReceived);
    }
}
