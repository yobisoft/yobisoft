using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using Yobisoft.Core.Extensions;

namespace Yobisoft.Tests.Core.Extension
{
    [TestClass]
    public class ArrayTest
    {
        [TestMethod]
        public void ArrayCopyFrom()
        {
            byte[] destination = new byte[3];
            byte[] source = { 1, 2 };
            destination.CopyFrom(source);
            Assert.AreEqual(source[0],  destination[0]);
            Assert.AreEqual(source[1],  destination[1]);
            Assert.AreEqual(0,          destination[2]);
        }

        [TestMethod]
        public void ArrayCopyFromIndex()
        {
            byte[] destination = new byte[3];
            byte[] source = { 1, 2 };
            destination.CopyFrom(source, 1);
            Assert.AreEqual(0, destination[0]);
            Assert.AreEqual(source[0], destination[1]);
            Assert.AreEqual(source[1], destination[2]);
        }

        [TestMethod]
        [ExpectedException(typeof(IndexOutOfRangeException))]
        public void ArrayCopyThrow()
        {
            byte[] destination = new byte[2];
            byte[] source = { 1, 2 };
            destination.CopyFrom(source, 1);
        }
    }
}
