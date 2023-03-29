using Microsoft.Diagnostics.Tracing.Parsers;
using Microsoft.Diagnostics.Tracing.Parsers.Kernel;
using Microsoft.Diagnostics.Tracing.Session;
using NetworkUsageTracker.Interfaces;
using System.Diagnostics;
using System.Net.NetworkInformation;

namespace NetworkUsageTracker
{
    internal class Program
    {
        const int msWait = 1000; //1800000; // ..30 minutes

        static readonly IUsageCollector usageCollector = new UsageCollector();
        static readonly IUsageListener usageListener = new UsageListener();
        static readonly IUsageAnalyzer usageAnalyzer = new UsageAnalyzer(usageCollector,usageListener);

        static void Main()
        {
            while (true)
            {
                Run();
                Thread.Sleep(msWait);
            }
        }

        static void Run()
        {
            var usageReporter = new UsageReporter(usageAnalyzer);
            usageReporter.ReportUsage();
        }
    }
}