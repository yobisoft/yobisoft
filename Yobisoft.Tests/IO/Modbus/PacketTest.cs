using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

using TestClass = Yobisoft.IO.Modbus.Packet;
using Helper = Yobisoft.IO.Helper;
using Function = Yobisoft.IO.Modbus.Function;
using ExceptionCode = Yobisoft.IO.Modbus.ExceptionCode;

namespace Yobisoft.Tests.IO.Modbus
{
    [TestClass]
    public class TestClassTest
    {

        [TestMethod]
        public void RegisterConverter()
        {
            ushort data = 0x0A01;
            byte[] ethalon = { 0x0A, 0x01 };
            Assert.IsTrue(ethalon.SequenceEqual(TestClass.RegisterConverter(data)));
        }

        [TestMethod]
        public void DataConverter()
        {
            ushort[] data = { 0x0A01, 0x0B02 };
            byte[] ethalon = { 0x0A, 0x01, 0x0B, 0x02 };
            Assert.IsTrue(ethalon.SequenceEqual(TestClass.DataConverter(data)));
        }

        [TestMethod]
        public void RegisterCountConverter()
        {
            ushort[] data = { 0x0A01, 0x0B02 };
            byte ethalon = 4;
            Assert.AreEqual(ethalon, TestClass.RegisterCountConverter(data));
        }

        [TestMethod]
        public void CoilDataConverter()
        {
            ushort[] data =
                { TestClass.CoilOn, TestClass.CoilOn,TestClass.CoilOn,TestClass.CoilOn,TestClass.CoilOn,TestClass.CoilOn,TestClass.CoilOn,TestClass.CoilOn,
                TestClass.CoilOff, TestClass.CoilOff,TestClass.CoilOff,TestClass.CoilOff,TestClass.CoilOff,TestClass.CoilOff,TestClass.CoilOff,TestClass.CoilOff,
                0,1,2,3,4
                };
            byte[] ethalon = { 0xFF, 0x00, 0x1E };
            Assert.IsTrue(ethalon.SequenceEqual(TestClass.CoilDataConverter(data)));
        }

        [TestMethod]
        public void CoilCountConverter()
        {
            ushort[] data =
               { TestClass.CoilOn, TestClass.CoilOn,TestClass.CoilOn,TestClass.CoilOn,TestClass.CoilOn,TestClass.CoilOn,TestClass.CoilOn,TestClass.CoilOn,
                TestClass.CoilOff, TestClass.CoilOff,TestClass.CoilOff,TestClass.CoilOff,TestClass.CoilOff,TestClass.CoilOff,TestClass.CoilOff,TestClass.CoilOff,
                0,1,2,3,4
                };
            byte ethalon = 3;
            Assert.AreEqual(ethalon, TestClass.CoilCountConverter(data));
        }

        [TestMethod]
        public void ReadCoilStatusRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.ReadCoilStatus;
            ushort address = 0x0002;
            ushort coilCount = 0x0004;
            byte[] ethalon = 
                { slaveAddress
                , (byte)function
                , Helper.Hi(address)
                , Helper.Lo(address)
                , Helper.Hi(coilCount)
                , Helper.Lo(coilCount) };
            TestClass test = TestClass.CreateReadRequest(slaveAddress, function, address, coilCount);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ReadCoilStatusResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.ReadCoilStatus;
            ushort[] data = { TestClass.CoilOn, TestClass.CoilOff, 0x7777 };
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , 1
                , 5 };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, data);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ReadInputStatusRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.ReadInputStatus;
            ushort address = 0x0002;
            ushort coilCount = 0x0004;
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , Helper.Hi(address)
                , Helper.Lo(address)
                , Helper.Hi(coilCount)
                , Helper.Lo(coilCount) };
            TestClass test = TestClass.CreateReadRequest(slaveAddress, function, address, coilCount);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ReadInputStatusResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.ReadInputStatus;
            ushort[] data = { TestClass.CoilOn, TestClass.CoilOff, 0x7777 };
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , 1
                , 5 };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, data);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ReadHoldingRegistersRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.ReadHoldingRegisters;
            ushort address = 0x0002;
            ushort regCount = 0x0004;
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , Helper.Hi(address)
                , Helper.Lo(address)
                , Helper.Hi(regCount)
                , Helper.Lo(regCount) };
            TestClass test = TestClass.CreateReadRequest(slaveAddress, function, address, regCount);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ReadHoldingRegistersResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.ReadHoldingRegisters;
            ushort[] data = { 0x2233, 0x4455, 0x6677 };
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , 6
                , 0x22, 0x33, 0x44, 0x55, 0x66, 0x77 };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, data);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ReadInputRegistersRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.ReadInputRegisters;
            ushort address = 0x0002;
            ushort regCount = 0x0004;
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , Helper.Hi(address)
                , Helper.Lo(address)
                , Helper.Hi(regCount)
                , Helper.Lo(regCount) };
            TestClass test = TestClass.CreateReadRequest(slaveAddress, function, address, regCount);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ReadInputRegistersResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.ReadInputRegisters;
            ushort[] data = { 0x2233, 0x4455, 0x6677 };
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , 6
                , 0x22, 0x33, 0x44, 0x55, 0x66, 0x77 };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, data);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ReadExceptionStatusRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.ReadExceptionStatus;
            byte[] ethalon = { slaveAddress, (byte)function };
            TestClass test = TestClass.CreateReadRequest(slaveAddress, function);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ReadExceptionStatusResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.ReadExceptionStatus;
            ushort data = 0x2233;
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , 0x22, 0x33 };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, data);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void DiagnosticRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.Diagnostic;
            ushort subfunction = 0x0002;
            ushort data = 0x0004;
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , Helper.Hi(subfunction)
                , Helper.Lo(subfunction)
                , Helper.Hi(data)
                , Helper.Lo(data) };
            TestClass test = TestClass.CreateReadRequest(slaveAddress, function, subfunction, data);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void DiagnosticResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.Diagnostic;
            ushort[] data = { 0x2233, 0x4455  };
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , 0x22, 0x33, 0x44, 0x55 };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, data);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void FetchCommEventCtrRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.FetchCommEventCtr;
            byte[] ethalon = { slaveAddress, (byte)function };
            TestClass test = TestClass.CreateReadRequest(slaveAddress, function);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void FetchCommEventCtrResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.FetchCommEventCtr;
            ushort[] data = { 0x2233, 0x4455 };
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , 0x22, 0x33, 0x44, 0x55 };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, data);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void FetchCommEventLogRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.FetchCommEventLog;
            byte[] ethalon = { slaveAddress, (byte)function };
            TestClass test = TestClass.CreateReadRequest(slaveAddress, function);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void FetchCommEventLogResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.FetchCommEventLog;
            ushort[] data = { 0x2233, 0x4455, 0x6677 };
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , 6
                , 0x22, 0x33, 0x44, 0x55, 0x66, 0x77 };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, data);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ForceSingleCoilRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.ForceSingleCoil;
            ushort address = 0x0002;
            /// no check for coil value
            ushort[] values = { TestClass.CoilOn, TestClass.CoilOff, 0x7777 };
            foreach (var value in values)
            {
                byte[] ethalon =
                    { slaveAddress
                    , (byte)function
                    , Helper.Hi(address)
                    , Helper.Lo(address)
                    , Helper.Hi(value)
                    , Helper.Lo(value) };
                TestClass test = TestClass.CreateWriteRequest(slaveAddress, function, address, value);
                Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
            }
        }

        [TestMethod]
        public void ForceSingleCoilResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.ForceSingleCoil;
            ushort address = 0x2233;
            // no check for coil value
            ushort count = 0x4455;
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , Helper.Hi(address)
                , Helper.Lo(address)
                , Helper.Hi(count)
                , Helper.Lo(count) };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, address, count);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void PresetSingleRegisterRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.PresetSingleRegister;
            ushort address = 0x0002;
            ushort value = 0x0003;
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , Helper.Hi(address)
                , Helper.Lo(address)
                , Helper.Hi(value)
                , Helper.Lo(value) };
            TestClass test = TestClass.CreateWriteRequest(slaveAddress, function, address, value);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void PresetSingleRegisterResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.PresetSingleRegister;
            ushort address = 0x2233;
            ushort count = 0x4455;
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , Helper.Hi(address)
                , Helper.Lo(address)
                , Helper.Hi(count)
                , Helper.Lo(count) };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, address, count);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ForceMultipleCoilsRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.ForceMultipleCoils;
            ushort address = 0x0002;
            ushort[] values = { TestClass.CoilOn, TestClass.CoilOff, 0x7777 };
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , Helper.Hi(address)
                , Helper.Lo(address)
                , Helper.Hi(3)
                , Helper.Lo(3)
                , 1
                , 0x05 };
            TestClass test = TestClass.CreateWriteRequest(slaveAddress, function, address, values);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void ForceMultipleCoilsResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.ForceMultipleCoils;
            ushort address = 0x2233;
            // no check for coil count
            ushort count = 0x4455;
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , Helper.Hi(address)
                , Helper.Lo(address)
                , Helper.Hi(count)
                , Helper.Lo(count) };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, address, count);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void PresetMultipleRegsRequest()
        {
            byte slaveAddress = 1;
            Function function = Function.PresetMultipleRegs;
            ushort address = 0x0002;
            ushort[] values = { TestClass.CoilOn, TestClass.CoilOff, 0x7777 };
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , Helper.Hi(address)
                , Helper.Lo(address)
                , Helper.Hi((ushort)values.Length)
                , Helper.Lo((ushort)values.Length)
                , (byte)(values.Length * 2)
                , Helper.Hi(values[0])
                , Helper.Lo(values[0])
                , Helper.Hi(values[1])
                , Helper.Lo(values[1])
                , Helper.Hi(values[2])
                , Helper.Lo(values[2])
            };
            TestClass test = TestClass.CreateWriteRequest(slaveAddress, function, address, values);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void PresetMultipleRegsResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.PresetMultipleRegs;
            ushort address = 0x2233;
            // no check for register count
            ushort count = 0x4455;
            byte[] ethalon =
                { slaveAddress
                , (byte)function
                , Helper.Hi(address)
                , Helper.Lo(address)
                , Helper.Hi(count)
                , Helper.Lo(count) };
            TestClass test = TestClass.CreateResponse(slaveAddress, function, address, count);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }

        [TestMethod]
        public void BadResponse()
        {
            byte slaveAddress = 1;
            Function function = Function.ReadCoilStatus;
            ExceptionCode code = ExceptionCode.IllegalFunction;
            byte[] ethalon =
                { slaveAddress
                , (byte)(0x80 | (byte)function)
                , (byte)code };
            TestClass test = TestClass.CreateBadResponse(slaveAddress, function, code);
            Assert.IsTrue(ethalon.SequenceEqual(test.Bytes));
        }
    }
}
