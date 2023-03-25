using Microsoft.Toolkit.Uwp.Notifications;
using NetworkUsageTracker.Interfaces;

namespace NetworkUsageTracker
{
    internal class UsageReporter : IUsageReporter
    {
        private IUsageAnalyzer _usageAnalyzer;

        public UsageReporter(IUsageAnalyzer usageAnalyzer)
        {
            _usageAnalyzer = usageAnalyzer;
        }

        public void ReportUsage()
        {
            var usageInfo = _usageAnalyzer.GetRelativeUsageInfo();
            var bytesSentInfo = Utilities.ConvertBytes(usageInfo.BytesSent);
            var bytesReceivedInfo = Utilities.ConvertBytes(usageInfo.BytesReceived);

            // .. Toast Notification
            ToastContentBuilder builder = new ToastContentBuilder();
            string toastMessage = "Hi Nathan! \n\nThere were " + bytesSentInfo + " sent and " + bytesReceivedInfo + " received";
            builder.AddText("Network Usage Tracker").AddText(toastMessage).Show();

            // .. Console report
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("===================");
            Console.WriteLine($"Data sent: {bytesSentInfo:F2}");
            Console.WriteLine($"Data received: {bytesReceivedInfo:F2}");
            Console.WriteLine();
        }
    }
}
