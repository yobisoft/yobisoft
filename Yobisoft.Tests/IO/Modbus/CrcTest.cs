using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using Yobisoft.IO.Modbus;
using System.Linq;
using Yobisoft.IO;

namespace Yobisoft.Tests.IO.Modbus
{
    [TestClass]
    public class CrcTest
    {
        private static readonly Dictionary<byte[], ushort> TestData = new Dictionary<byte[], ushort>()
            {
                { new byte[]{ 0 }, 0x40BF },
                { new byte[]{ 1 }, 0x807E },
                { new byte[]{ 0x04, 0x01, 0x00, 0x0A, 0x00, 0x0D }, 0x98DD },
                { new byte[]{ 0x04, 0x01, 0x02, 0x0A, 0x11 }, 0x50B3 },
            };

        [TestMethod]
        public void Calculate()
        {
            foreach (var test in TestData)
                Assert.AreEqual(test.Value, Crc.Calculate(test.Key));
        }

        [TestMethod]
        public void FastCalculate()
        {
            foreach (var test in TestData)
                Assert.AreEqual(test.Value, Crc.FastCalculate(test.Key));
        }

        [TestMethod]
        public void MutualTest()
        {
            foreach (var b in Enumerable.Range(ushort.MinValue, ushort.MaxValue))
            {
                var data = new[] { Helper.Hi((ushort)b), Helper.Lo((ushort)b) };
                Assert.AreEqual(Crc.Calculate(data), Crc.FastCalculate(data));
            }
        }
    }
}
