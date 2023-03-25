using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUsageTracker
{
    public struct UsageDisplayInfo
    {
        public readonly string Name;
        public readonly double Value;

        public UsageDisplayInfo(string name, double value)
        {
            Name = name;
            Value = value;
        }

        public override string ToString() => Math.Round(Value, 2).ToString() + Name.ToString();
    }

    internal class Utilities
    {
        private Utilities() { }

        public static UsageDisplayInfo ConvertBytes(double bytes)
        {
            if (bytes >= 1048575)
                return new UsageDisplayInfo("mb", bytes / 1024 / 1024);
            else if (bytes >= 1023)
                return new UsageDisplayInfo("kb", bytes / 1024);
            else
                return new UsageDisplayInfo(" bytes", bytes);
        }
    }
}
