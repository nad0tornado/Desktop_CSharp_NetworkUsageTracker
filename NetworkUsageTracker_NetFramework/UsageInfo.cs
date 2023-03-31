namespace NetworkUsageTracker
{
    internal struct UsageInfo
    {
        public long BytesSent { get; set; }
        public long BytesReceived { get; set; }

        public UsageInfo(long sent, long received)
        {
            BytesSent = sent;
            BytesReceived = received;
        }
    }
}
