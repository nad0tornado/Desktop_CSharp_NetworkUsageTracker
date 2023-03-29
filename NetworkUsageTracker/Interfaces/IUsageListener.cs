using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUsageTracker.Interfaces
{
    internal interface IUsageListener
    {
        private TraceEventSession? kernelSession { get => null; }

        public Action<TcpIpTraceData> ReceiveTCP { get; set; }
        public Action<UdpIpTraceData> ReceiveUDP { get; set; }
        public Action<TcpIpSendTraceData> SendTCP { get; set; }
        public Action<UdpIpTraceData> SendUDP { get; set; }

        public void Start();
    }
}
