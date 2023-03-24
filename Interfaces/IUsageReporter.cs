using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUsageTracker.Interfaces
{
    internal interface IUsageReporter
    {
        public void ReportUsage(UsageInfo usageInfo);
    }
}
