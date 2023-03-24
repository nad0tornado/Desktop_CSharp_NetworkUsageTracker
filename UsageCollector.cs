using NetworkUsageTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUsageTracker
{
    /// <summary>
    /// Collects information from the system's Network Interfaces regarding the total number of bytes sent + received
    /// </summary>
    internal class UsageCollector : IUsageCollector
    {
        public UsageInfo GetUsageInfo()
        {
            long bytesSent = 0;
            long bytesReceived = 0;

            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                bytesSent += ni.GetIPv4Statistics().BytesSent;
                bytesReceived += ni.GetIPv4Statistics().BytesReceived;
            }

            return new UsageInfo(bytesSent, bytesReceived);
        }
    }
}
