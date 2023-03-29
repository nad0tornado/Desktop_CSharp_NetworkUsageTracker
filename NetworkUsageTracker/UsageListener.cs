using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;
using NetworkUsageTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUsageTracker
{
    internal class UsageListener : IUsageListener
    {
        private TraceEventSession kernelSession;

        public Action<TcpIpTraceData> ReceiveTCP { get; set; } = new Action<TcpIpTraceData>((data) => { });
        public Action<UdpIpTraceData> ReceiveUDP { get; set; } = new Action<UdpIpTraceData>((data) => { });
        public Action<TcpIpSendTraceData> SendTCP { get; set; } = new Action<TcpIpSendTraceData>((data) => { });
        public Action<UdpIpTraceData> SendUDP { get; set; } = new Action<UdpIpTraceData>((data) => { });

        public UsageListener()
        {
            kernelSession = new TraceEventSession(KernelTraceEventParser.KernelSessionName);

            kernelSession.EnableKernelProvider(KernelTraceEventParser.Keywords.NetworkTCPIP);

            kernelSession.Source.Kernel.TcpIpRecv += (data) => ReceiveTCP(data);
            kernelSession.Source.Kernel.UdpIpRecv += (data) => ReceiveUDP(data);
            kernelSession.Source.Kernel.TcpIpSend += (data) => SendTCP(data);
            kernelSession.Source.Kernel.UdpIpSend += (data) => SendUDP(data);
        }

        public void Start()
        {
            kernelSession.Source.Process();
        }
    }
}
