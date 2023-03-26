using NetworkUsageTracker.Interfaces;

namespace NetworkUsageTracker
{
    internal interface IUsageAnalyzer
    {
        /// <summary>
        /// Get the total number of bytes sent + received since the last call
        /// </summary>
        /// <returns></returns>
        public UsageInfo GetRelativeUsageInfo();
        public UsageInfo GetAverageUsageInfo();
    }
}
