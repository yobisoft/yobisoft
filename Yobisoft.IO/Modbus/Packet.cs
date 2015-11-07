using System;
using System.Collections.Generic;
using System.Linq;
using Yobisoft.Core.Extensions;

namespace Yobisoft.IO.Modbus
{
    /// <summary>
    /// Modbus packet
    /// </summary>
    public sealed class Packet
    {
        /// <summary>
        /// Coil on status constant
        /// </summary>
        public const ushort CoilOn = 0xFF00;

        /// <summary>
        /// Coil off status constant
        /// </summary>
        public const ushort CoilOff = 0x0000;

        /// <summary>
        /// Get packet's bytes
        /// </summary>
        public IEnumerable<byte> Bytes => _packetBytes.AsEnumerable();

        /// <summary>
        /// Creates an instance of modbus packet
        /// </summary>
        /// <param name="length">Length of a packet</param>
        private Packet(int length)
        {
            if (length < 2) throw new ArgumentException(nameof(length));
            _packetBytes = new byte[length];
        }

        /// <summary>
        /// Gets or sets device address
        /// </summary>
        public byte Device
        {
            get { return _packetBytes[0]; }
            set { _packetBytes[0] = value; }
        }

        /// <summary>
        /// Gets or sets function number
        /// </summary>
        public byte FunctionNumber
        {
            get { return _packetBytes[1]; }
            set { _packetBytes[1] = value; }
        }

        /// <summary>
        /// Gets data
        /// </summary>
        public IEnumerable<byte> Data => _packetBytes.Skip(2);

        /// <summary>
        /// Creates custom modbus packet 
        /// </summary>
        /// <remarks>No checks</remarks>
        /// <param name="device">Slave address</param>
        /// <param name="function">Function number</param>
        /// <param name="data">Function data</param>
        /// <returns>Modbus packet</returns>
        public static Packet CreateCustomPacket(byte device, byte function, IEnumerable<byte> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            var result = new Packet(data.Count() + 2)
            {
                Device = device,
                FunctionNumber = (byte)function
            };
            result.CopyData(data);
            return result;
        }

        /// <summary>
        /// Creates packet from bytes
        /// </summary>
        /// <remarks>No checks</remarks>
        /// <param name="bytes">Byte sequence</param>
        /// <returns>Modbus packet</returns>
        public static Packet FromBytes(IEnumerable<byte> bytes)
        {
            if (bytes != null) throw new ArgumentNullException(nameof(bytes));
            if (bytes.Count() < 2) throw new ArgumentException(nameof(bytes));
            return CreateCustomPacket(bytes.First(), bytes.Skip(1).First(), bytes.Skip(2));
        }

        /// <summary>
        /// Creates modbus read data request
        /// </summary>
        /// <param name="device">Slave address</param>
        /// <param name="function">Function</param>
        /// <param name="data">Function arguments</param>
        /// <returns>Modbus read data request</returns>
        public static Packet CreateReadRequest(byte device, Function function, params ushort[] data)
        {
            return CreatePacket(RequestCheckData, RequestConvertData, device, function, null, data);
        }

        /// <summary>
        /// Creates modbus write data request
        /// </summary>
        /// <param name="device">Slave address</param>
        /// <param name="function">Function</param>
        /// <param name="startAddress">Starting address</param>
        /// <param name="data">Data to write</param>
        /// <returns>Modbus write data request</returns>
        public static Packet CreateWriteRequest(byte device, Function function, ushort startAddress, params ushort[] data)
        {
            return CreatePacket(RequestCheckData, RequestConvertData, device, function, startAddress, data);
        }

        /// <summary>
        /// Creates modbus read data response
        /// </summary>
        /// <param name="device">Slave address</param>
        /// <param name="function">Function</param>
        /// <param name="data">Reponse data</param>
        /// <returns>Modbus read data response</returns>
        public static Packet CreateResponse(byte device, Function function, params ushort[] data)
        {
            return CreatePacket(ResponseCheckData, ResponseConvertData, device, function, null, data);
        }

        /// <summary>
        /// Creates modbus bad response
        /// </summary>
        /// <param name="device">Slave address</param>
        /// <param name="function">Function</param>
        /// <param name="code">Exception code</param>
        /// <returns>Modbus bad response</returns>
        public static Packet CreateBadResponse(byte device, Function function, ExceptionCode code)
        {
            return CreateCustomPacket(device, (byte)(0x80 | (byte)function), new[] { (byte)code });
        }

        /// <summary>
        /// Gets bytes of a register in modbus order
        /// </summary>
        /// <param name="value">Register value</param>
        /// <returns>Bytes of a register in modbus order</returns>
        public static IEnumerable<byte> RegisterConverter(ushort value)
        {
            yield return Helper.Hi(value);
            yield return Helper.Lo(value);
        }

        /// <summary>
        /// Gets bytes of registers in modbus order
        /// </summary>
        /// <param name="values">Registers' values</param>
        /// <returns>Bytes of a registers in modbus order</returns>
        public static IEnumerable<byte> DataConverter(ushort[] values)
        {
            return values.SelectMany(RegisterConverter);
        }

        /// <summary>
        /// Gets bytes of coils in modbus order
        /// </summary>
        /// <remarks>
        /// Each array item represents one coil value. Zero values are converted to 0. Non-zero values are converted to 1.
        /// </remarks>
        /// <param name="values">Coils' values</param>
        /// <returns>Bytes of coils in modbus order</returns>
        public static IEnumerable<byte> CoilDataConverter(ushort[] values)
        {
            byte result = 0;
            int index = 0;
            foreach (var value in values)
            {
                // treats all non zero values as on status
                result |= (byte)(((value == Packet.CoilOff) ? 0 : 1) << index);
                if (++index % 8 == 0)
                {
                    yield return result;
                    index = 0;
                    result = 0;
                }
            }
            if (index != 0) yield return result;
        }

        /// <summary>
        /// Gets register count in bytes
        /// </summary>
        /// <param name="values">Registers' values</param>
        /// <returns>Register count in bytes</returns>
        public static byte RegisterCountConverter(ushort[] values)
        {
            return (byte)(values.Length * 2);
        }

        /// <summary>
        /// Gets coil count in bytes
        /// </summary>
        /// <param name="values">Coils' values</param>
        /// <returns>Coil count in bytes</returns>
        public static byte CoilCountConverter(ushort[] values)
        {
            return (byte)((values.Length + 7) / 8);
        }

        private static readonly Dictionary<Function, Converter<ushort[], bool>> RequestCheckData 
            = new Dictionary<Function, Converter<ushort[], bool>>()
        {
                // read functions
            { Function.ReadCoilStatus       , data => data.Length == 2 },
            { Function.ReadInputStatus      , data => data.Length == 2 },
            { Function.ReadHoldingRegisters , data => data.Length == 2 },
            { Function.ReadInputRegisters   , data => data.Length == 2 },
            { Function.ReadExceptionStatus  , data => data.Length == 0 },
            { Function.Diagnostic           , data => data.Length == 2 },
            { Function.FetchCommEventCtr    , data => data.Length == 0 },
            { Function.FetchCommEventLog    , data => data.Length == 0 },
                // write functions
            { Function.ForceSingleCoil      , data => data.Length == 1 },
            { Function.PresetSingleRegister , data => data.Length == 1 },
            { Function.ForceMultipleCoils   , coils => coils.Length < 123 * 8 },
            { Function.PresetMultipleRegs   , regs => regs.Length < 123 },
        };

        private static readonly Dictionary<Function, Converter<ushort[], bool>> ResponseCheckData
            = new Dictionary<Function, Converter<ushort[], bool>>()
        {
                // read functions
            { Function.ReadCoilStatus       , data => data.Length > 0 },
            { Function.ReadInputStatus      , data => data.Length > 0 },
            { Function.ReadHoldingRegisters , data => data.Length > 0 },
            { Function.ReadInputRegisters   , data => data.Length > 0 },
            { Function.ReadExceptionStatus  , data => data.Length == 1 },
            { Function.Diagnostic           , data => data.Length == 2 },
            { Function.FetchCommEventCtr    , data => data.Length == 2 },
            { Function.FetchCommEventLog    , data => data.Length >= 3 },
                // write functions
            { Function.ForceSingleCoil      , data => data.Length == 2 },
            { Function.PresetSingleRegister , data => data.Length == 2 },
            { Function.ForceMultipleCoils   , data => data.Length == 2 },
            { Function.PresetMultipleRegs   , data => data.Length == 2 },
        };

        private static readonly Dictionary<Function, Converter<Param, IEnumerable<byte>>> RequestConvertData
            = new Dictionary<Function, Converter<Param, IEnumerable<byte>>>()
        {
                // read functions
            { Function.ReadCoilStatus       , data => DataConverter(data.Data) },
            { Function.ReadInputStatus      , data => DataConverter(data.Data) },
            { Function.ReadHoldingRegisters , data => DataConverter(data.Data) },
            { Function.ReadInputRegisters   , data => DataConverter(data.Data) },
            { Function.ReadExceptionStatus  , data => DataConverter(data.Data) },
            { Function.Diagnostic           , data => DataConverter(data.Data) },
            { Function.FetchCommEventCtr    , data => DataConverter(data.Data) },
            { Function.FetchCommEventLog    , data => DataConverter(data.Data) },
                // write functions
            { Function.ForceSingleCoil      , data => ParamConverter(data, DataConverter) },
            { Function.PresetSingleRegister , data => ParamConverter(data, DataConverter) },
            { Function.ForceMultipleCoils   , data => ParamConverter(data, CoilDataConverter, CoilCountConverter, RegisterConverter) },
            { Function.PresetMultipleRegs   , data => ParamConverter(data, DataConverter, RegisterCountConverter, RegisterConverter) },
        };

        private static readonly Dictionary<Function, Converter<Param, IEnumerable<byte>>> ResponseConvertData
            = new Dictionary<Function, Converter<Param, IEnumerable<byte>>>()
        {
                // read functions
            { Function.ReadCoilStatus       , data => ParamConverter(data, CoilDataConverter, CoilCountConverter) },
            { Function.ReadInputStatus      , data => ParamConverter(data, CoilDataConverter, CoilCountConverter) },
            { Function.ReadHoldingRegisters , data => ParamConverter(data, DataConverter, RegisterCountConverter) },
            { Function.ReadInputRegisters   , data => ParamConverter(data, DataConverter, RegisterCountConverter) },
            { Function.ReadExceptionStatus  , data => ParamConverter(data, DataConverter) },
            { Function.Diagnostic           , data => ParamConverter(data, DataConverter) },
            { Function.FetchCommEventCtr    , data => ParamConverter(data, DataConverter) },
            { Function.FetchCommEventLog    , data => ParamConverter(data, DataConverter, RegisterCountConverter) },
                // write functions
            { Function.ForceSingleCoil      , data => DataConverter(data.Data) },
            { Function.PresetSingleRegister , data => DataConverter(data.Data) },
            { Function.ForceMultipleCoils   , data => DataConverter(data.Data) },
            { Function.PresetMultipleRegs   , data => DataConverter(data.Data) },
        };

        private struct Param
        {
            public ushort? StartAddress;
            public ushort[] Data;
            public ushort Length => (ushort)Data.Length;
        }

        private static IEnumerable<byte> ParamConverter
            (Param data
            , Converter<ushort[], IEnumerable<byte>> dataConverter
            , Converter<ushort[], byte> countConverter = null
            , Converter<ushort, IEnumerable<byte>> dataLengthConverter = null
)
        {
            if (data.StartAddress != null)
                foreach (var result in RegisterConverter((ushort)data.StartAddress))
                    yield return result;
            if (countConverter != null)
            {
                if (dataLengthConverter != null)
                    foreach (var result in dataLengthConverter(data.Length))
                        yield return result;
                yield return countConverter(data.Data);
            }
            foreach (var result in dataConverter(data.Data))
                yield return result;
        }

        private static Packet CreatePacket
            ( Dictionary<Function, Converter<ushort[], bool>> checkData
            , Dictionary<Function, Converter<Param, IEnumerable<byte>>> convertData
            , byte device
            , Function function
            , ushort? startAddress
            , params ushort[] data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            Converter<ushort[], bool> check;
            if (checkData.TryGetValue(function, out check))
            {
                var p = new Param { StartAddress = startAddress, Data = data };
                if (check(p.Data))
                {
                    Converter<Param, IEnumerable<byte>> convert;
                    if (convertData.TryGetValue(function, out convert))
                        return CreateCustomPacket(device, (byte)function, convert(p));
                }
                else
                    throw new ArgumentException(nameof(data));
            }
            throw new ArgumentException(nameof(function));
        }

        private void CopyData(IEnumerable<byte> data)
        {
            _packetBytes.CopyFrom(data, 2);
        }

        private readonly byte[] _packetBytes;
    }
}
