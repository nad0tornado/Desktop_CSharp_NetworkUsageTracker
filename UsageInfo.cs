namespace NetworkUsageTracker
{
    internal struct UsageInfo
    {
        public long BytesSent { get; }
        public long BytesReceived { get; }

        public UsageInfo(long sent, long received)
        {
            BytesSent = sent;
            BytesReceived = received;
        }
    }
}
