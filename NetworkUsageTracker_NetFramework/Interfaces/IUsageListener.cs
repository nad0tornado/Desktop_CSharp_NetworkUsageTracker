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
        Action<TcpIpTraceData> ReceiveTCP { get; set; }
        Action<UdpIpTraceData> ReceiveUDP { get; set; }
        Action<TcpIpSendTraceData> SendTCP { get; set; }
        Action<UdpIpTraceData> SendUDP { get; set; }

        void Start();
    }
}
