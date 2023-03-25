namespace NetworkUsageTracker
{
    internal class Program
    {
        const int mWait = 30;
        const int sWait = 0;
        const int msWait = 6000; //1800000; // ..30 minutes
            //mWait > 0 ? mWait * 60 * 1000 : sWait > 0 ? sWait * 1000 : 5000;
        static DateTime lastRunTime = DateTime.Now - TimeSpan.FromMilliseconds(msWait);

        static void Main(string[] args)
        {
            while(true)
            {
                Run();
            }
        }

        static void Run()
        {
            var usageCollector = new UsageCollector();
            var usageAnalyzer = new UsageAnalyzer(usageCollector);
            var usageReporter = new UsageReporter(usageAnalyzer);
            usageReporter.ReportUsage();

            // .. Wait and then run again
            Thread.Sleep(msWait);
        }
    }
}