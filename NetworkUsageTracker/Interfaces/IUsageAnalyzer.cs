using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using NetworkUsageTracker.Interfaces;

namespace NetworkUsageTracker
{
    internal interface IUsageAnalyzer
    {
        public Dictionary<string, UsageInfo> AppUsage { get; }

        public UsageInfo GetRelativeUsageInfo();
        public UsageInfo GetAverageUsageInfo();

        void HandleReceiveTCP(TcpIpTraceData data);
        void HandleReceiveUDP(UdpIpTraceData data);
        void HandleSendTCP(TcpIpSendTraceData data);
        void HandleSendUDP(UdpIpTraceData data);

        public void ClearAppUsage();
    }
}
