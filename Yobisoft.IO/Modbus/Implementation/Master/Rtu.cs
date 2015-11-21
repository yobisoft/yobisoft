using System.Collections.Generic;
using System.Linq;

namespace Yobisoft.IO.Modbus.Implementation.Master
{
    /// <summary>
    /// Modbus RTU client implementation
    /// </summary>
    internal class Rtu
        : Master<Packet>
    {
        public override IPacket Query(Packet packet)
        {
            Port.Send(AddCrc(packet));
            int responseSize = Packet.GetResponseSize(packet);
            if (responseSize != 0)
            {
                IEnumerable<byte> read = Port.Receive(responseSize + 2);
                Packet result = Packet.FromBytes(read.Take(responseSize));
                return result;
            }
            return null;
        }

        private IEnumerable<byte> AddCrc(IPacket packet)
        {
            foreach (var result in packet.Bytes)
                yield return result;
            ushort crc = Crc.FastCalculate(packet.Bytes);
            foreach (var result in Packet.RegisterConverter(crc))
                yield return result;
        }
    }
}
