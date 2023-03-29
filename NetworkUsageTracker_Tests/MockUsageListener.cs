using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using NetworkUsageTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUsageTracker_Tests
{
    internal class MockUsageListener : IUsageListener
    {
        public Action<TcpIpTraceData> ReceiveTCP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action<UdpIpTraceData> ReceiveUDP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action<TcpIpSendTraceData> SendTCP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public Action<UdpIpTraceData> SendUDP { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public void Start()
        {
            throw new NotImplementedException();
        }
    }
}
