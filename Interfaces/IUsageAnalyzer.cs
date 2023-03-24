
using NetworkUsageTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUsageTracker
{
    internal interface IUsageAnalyzer
    {
        /// <summary>
        /// Get the total number of bytes sent + received since the last call
        /// </summary>
        /// <returns></returns>
        public UsageInfo GetRelativeUsageInfo();
    }
}
