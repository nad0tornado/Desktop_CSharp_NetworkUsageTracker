using System.Net.NetworkInformation;
using Microsoft.Toolkit.Uwp.Notifications;
using NetworkUsageTracker.Interfaces;

namespace NetworkUsageTracker
{
    internal class Program
    {
        

        const int mWait = 30;
        const int sWait = 0;
        const int msWait = 6000; //1800000; // ..30 minutes
            //mWait > 0 ? mWait * 60 * 1000 : sWait > 0 ? sWait * 1000 : 5000;
        static DateTime lastRunTime = DateTime.Now - TimeSpan.FromMilliseconds(msWait);

        private struct MeasurementInfo
        {
            public readonly string Name;
            public readonly double Value;

            public MeasurementInfo(string name, double value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString() => Math.Round(Value,2).ToString() + Name.ToString();
        }

        static void Main(string[] args)
        {
            while(true)
            {
                Run();
            }
        }

        static void Run()
        {
            // .. Usage Calculation
            var usageCollector = new UsageCollector();
            var byteTracker = new UsageAnalyzer(usageCollector);
            var usageInfo = byteTracker.GetRelativeUsageInfo();
            var usageReporter = new UsageReporter();
            usageReporter.ReportUsage(usageInfo);

            // .. Wait and then run again
            Thread.Sleep(msWait);
        }

        static MeasurementInfo ConvertBytes(long bytes)
        {
            if (bytes >= 1000000)
                return new MeasurementInfo("Mb", bytes / 1024.0 / 1024.0);
            else if (bytes >= 1000)
                return new MeasurementInfo("Kb", bytes / 1024.0);
            else
                return new MeasurementInfo("b", bytes);
        }
    }
}