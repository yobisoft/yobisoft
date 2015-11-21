using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;

namespace Yobisoft.IO.Ports.Implementation.Client
{
    internal sealed class Serial
        : ClientPort
    {
        private SerialPort Port { get; } = new SerialPort();

        public override IEnumerable<byte> Receive(int count)
        {
            byte[] result = new byte[count];
            int readCount = Port.Read(result, 0, count);
            return result.Take(readCount);
        }

        public override void Send(IEnumerable<byte> data)
        {
            if (data == null) throw new ArgumentNullException(nameof(data));
            byte[] buffer = data.ToArray();
            Port.Write(buffer, 0 , buffer.Length);
        }
    }
}
