using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using NetworkUsageTracker.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

namespace NetworkUsageTracker
{
    internal class UsageAnalyzer: IUsageAnalyzer
    {
        private long lastBytesSent = 0, lastBytesReceived = 0;

        private List<long> pastBytesSent = new List<long>();
        private List<long> pastBytesReceived = new List<long>();

        private IUsageCollector _usageCollector;
        private IUsageListener _usageListener;

        private Dictionary<string, UsageInfo> _appUsage = new Dictionary<string, UsageInfo>();
        public Dictionary<string, UsageInfo> AppUsage { 
            get => _appUsage.ToDictionary(e => e.Key, e => e.Value); 
            private set => _appUsage = value; 
        }

        public UsageAnalyzer(IUsageCollector collector, IUsageListener listener)
        {
            _usageCollector = collector;
            _usageListener = listener;

            _usageListener.ReceiveTCP = HandleReceiveTCP;
            _usageListener.ReceiveUDP = HandleReceiveUDP;
            _usageListener.SendTCP = HandleSendTCP;
            _usageListener.SendUDP = HandleSendUDP;

            Task.Run(() => _usageListener.Start());
        }

        private void HandleSend(string processName, int bytes)
        {
            if (!_appUsage.ContainsKey(processName))
                _appUsage.Add(processName, new UsageInfo());

            var processUsage = _appUsage[processName];
            processUsage.BytesSent = bytes;
            _appUsage[processName] = processUsage;
        }

        private void HandleReceive(string processName, int bytes)
        {
            if (!_appUsage.ContainsKey(processName))
                _appUsage.Add(processName, new UsageInfo());

            var processUsage = _appUsage[processName];
            processUsage.BytesReceived = bytes;
            _appUsage[processName] = processUsage;
        }

        public void HandleReceiveTCP(TcpIpTraceData data) => HandleReceive(data.ProcessName, data.size);
        public void HandleReceiveUDP(UdpIpTraceData data) => HandleReceive(data.ProcessName, data.size);
        public void HandleSendTCP(TcpIpSendTraceData data) => HandleSend(data.ProcessName, data.size);
        public void HandleSendUDP(UdpIpTraceData data) => HandleSend(data.ProcessName, data.size);

        public void ClearAppUsage() => _appUsage.Clear();

        public UsageInfo GetRelativeUsageInfo()
        {
            if (_usageCollector == null)
                throw new NullReferenceException("A UsageCollector is required to get relative usage info");

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
