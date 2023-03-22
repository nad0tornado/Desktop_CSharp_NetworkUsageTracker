using System.Net.NetworkInformation;
using Microsoft.Toolkit.Uwp.Notifications;

namespace NetworkUsageTracker
{
    internal class Program
    {
        static long bytesSent_last = 0;
        static long bytesReceived_last = 0;

        const int mWait = 30;
        const int sWait = 0;
        const int msWait = 1800000; // ..30 minutes
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
            long bytesSent = 0;
            long bytesReceived = 0;

            NetworkInterface[] interfaces = NetworkInterface.GetAllNetworkInterfaces();
            foreach (NetworkInterface ni in interfaces)
            {
                bytesSent += ni.GetIPv4Statistics().BytesSent;
                bytesReceived += ni.GetIPv4Statistics().BytesReceived;
            }

            long totalBytesSent = bytesSent - bytesSent_last; //(bytesSent / 1024.0 / 1024.0)-mBytesSent_last;
            long totalBytesReceived = bytesReceived - bytesReceived_last; //(bytesReceived / 1024.0 / 1024.0)-mBytesReceived_last;

            var bytesSentInfo = ConvertBytes(totalBytesSent);
            var bytesReceivedInfo = ConvertBytes(totalBytesReceived);
            
            bytesSent_last = totalBytesSent;
            bytesReceived_last = totalBytesReceived;

            // .. Toast Notification
            ToastContentBuilder builder = new ToastContentBuilder();
            string toastMessage = "Hi Nathan! \n\nThere were " + bytesSentInfo + " sent and " + bytesReceivedInfo + " received since "+ lastRunTime;
            builder.AddText("Network Usage Tracker").AddText(toastMessage).Show();

            lastRunTime = DateTime.Now;

            // .. Console report
            Console.WriteLine(lastRunTime);
            Console.WriteLine("===================");
            Console.WriteLine($"Data sent: {bytesSentInfo:F2} MB");
            Console.WriteLine($"Data received: {bytesReceivedInfo:F2} MB");
            Console.WriteLine();

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