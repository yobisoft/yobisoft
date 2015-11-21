using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Yobisoft.Tests
{
    [TestClass]
    public class Yobisoft
    {
        [TestMethod]
        public void SumInt()
        {
            int? i1 = null;
            int i2 = 2;
            int? ethalon = null;
            Assert.AreEqual(ethalon, i1 + i2);
        }
    }
}
