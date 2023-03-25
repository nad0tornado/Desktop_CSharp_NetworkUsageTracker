using NetworkUsageTracker;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetworkUsageTracker_Tests
{
    public class UtilitiesUnitTests
    {
        [Theory]
        [InlineData("kb",1,1)]
        [InlineData("kb",1.333,1.33)]
        public void TestUsageDisplayInfoToString(string name, double value, double expectedRoundedValue)
        {
            var usageDisplayInfo = new UsageDisplayInfo(name, value);
            Assert.Equal(expectedRoundedValue+name,usageDisplayInfo.ToString());
        }

        [Theory]
        [InlineData(1,1)]
        [InlineData(1024, 1)]
        [InlineData(1048576, 1)]
        public void TestConvertBytes(long bytesInput,double expectedValue)
        {
            var usageDisplayInfo = Utilities.ConvertBytes(bytesInput);
            Assert.Equal(expectedValue, usageDisplayInfo.Value);
        }

        [Theory]
        [InlineData(1, "1 bytes")]
        [InlineData(1024, "1kb")]
        [InlineData(1048576, "1mb")]
        public void TestConvertAndDisplayBytes(long bytesInput, string expectedValue)
        {
            var usageDisplayInfo = Utilities.ConvertBytes(bytesInput);
            Assert.Equal(expectedValue.ToLower(), usageDisplayInfo.ToString().ToLower());
        }
    }
}
