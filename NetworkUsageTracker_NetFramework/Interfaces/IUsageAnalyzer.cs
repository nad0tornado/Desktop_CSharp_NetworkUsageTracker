using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using NetworkUsageTracker.Interfaces;
using System.Collections.Generic;

namespace NetworkUsageTracker
{
    internal interface IUsageAnalyzer
    {
        Dictionary<string, UsageInfo> AppUsage { get; }

        UsageInfo GetRelativeUsageInfo();
        UsageInfo GetAverageUsageInfo();

        void HandleReceiveTCP(TcpIpTraceData data);
        void HandleReceiveUDP(UdpIpTraceData data);
        void HandleSendTCP(TcpIpSendTraceData data);
        void HandleSendUDP(UdpIpTraceData data);

        void ClearAppUsage();
    }
}
