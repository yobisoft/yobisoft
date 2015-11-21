using System.Collections.Generic;

namespace Yobisoft.IO
{
    public interface IPacket
    {
        /// <summary>
        /// Packet bytes
        /// </summary>
        IEnumerable<byte> Bytes { get; }
    }
}
