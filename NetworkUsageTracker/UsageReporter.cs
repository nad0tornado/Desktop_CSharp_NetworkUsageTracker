﻿using Microsoft.Toolkit.Uwp.Notifications;
using NetworkUsageTracker.Interfaces;

namespace NetworkUsageTracker
{
    internal class UsageReporter
    {
        private IUsageAnalyzer _usageAnalyzer;

        public UsageReporter(IUsageAnalyzer usageAnalyzer)
        {
            _usageAnalyzer = usageAnalyzer;
        }

        public void ReportUsage()
        {
            var usageInfo = _usageAnalyzer.GetRelativeUsageInfo();
            var averageUsageInfo = _usageAnalyzer.GetAverageUsageInfo();

            var bytesSent = Utilities.ConvertBytes(usageInfo.BytesSent);
            var bytesReceived = Utilities.ConvertBytes(usageInfo.BytesReceived);
            var avgBytesSent = Utilities.ConvertBytes(averageUsageInfo.BytesSent);
            var avgBytesReceived = Utilities.ConvertBytes(averageUsageInfo.BytesReceived);

            var bytesSentAboveAverage = Utilities.ConvertBytes(usageInfo.BytesSent - averageUsageInfo.BytesSent);
            var bytesReceivedAboveAverage = Utilities.ConvertBytes(averageUsageInfo.BytesReceived - averageUsageInfo.BytesReceived);

            // .. Only toast if the usage is above average
            if (bytesSentAboveAverage.Value > 0 || bytesReceivedAboveAverage.Value > 0)
            {
                ToastContentBuilder builder = new ToastContentBuilder();

                string toastMessage = "Hi Nathan! \n\n" +
                    (bytesSentAboveAverage.Value > 0 ? "Sent: " + bytesSentAboveAverage + " above average \n" : "") +
                    (bytesReceivedAboveAverage.Value > 0 ? "Received: " + bytesReceivedAboveAverage + " above average" : "");

                builder.AddText("Network Usage Tracker").AddText(toastMessage).Show();
            }

            // .. Console report
            Console.Clear();
            Console.WriteLine(DateTime.Now);
            Console.WriteLine("===================");
            Console.WriteLine($"Data sent: {bytesSent:F2} (Avg: {avgBytesSent})");
            Console.WriteLine($"Data received: {bytesReceived:F2} (Avg: {avgBytesReceived})");
            Console.WriteLine("--");
            foreach (var processUsageInfo in _usageAnalyzer.AppUsage.Where(a => a.Value.BytesReceived > 50))
            {
                Console.WriteLine(processUsageInfo.Key + ":");

                var processBytesSent = Utilities.ConvertBytes(processUsageInfo.Value.BytesSent);
                var processBytesReceived = Utilities.ConvertBytes(processUsageInfo.Value.BytesReceived);

                Console.WriteLine(" - " + processBytesSent + " sent");
                Console.WriteLine(" - " + processBytesReceived + " received");
            }
            Console.WriteLine("--");
            _usageAnalyzer.ClearAppUsage();
        }
    }
}
