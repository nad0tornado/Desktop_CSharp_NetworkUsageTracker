using Microsoft.Toolkit.Uwp.Notifications;
using NetworkUsageTracker.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUsageTracker
{
    internal class UsageReporter : IUsageReporter
    {
        private struct MeasurementInfo
        {
            public readonly string Name;
            public readonly double Value;

            public MeasurementInfo(string name, double value)
            {
                Name = name;
                Value = value;
            }

            public override string ToString() => Math.Round(Value, 2).ToString() + Name.ToString();
        }

        public void ReportUsage(UsageInfo usageInfo)
        {
            var bytesSentInfo = ConvertBytes(usageInfo.BytesSent);
            var bytesReceivedInfo = ConvertBytes(usageInfo.BytesReceived);

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

        private MeasurementInfo ConvertBytes(long bytes)
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
