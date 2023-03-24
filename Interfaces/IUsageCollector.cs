using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUsageTracker.Interfaces
{
    /// <summary>
    /// Collects information from the system's Network Interfaces regarding the total number of bytes sent + received
    /// </summary>
    internal interface IUsageCollector
    {
        /// <summary>
        /// Get the total number of bytes sent + received since the last Network Interface reset
        /// </summary>
        /// <returns></returns>
        public UsageInfo GetUsageInfo();
    }
}
