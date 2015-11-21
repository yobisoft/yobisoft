using System;
using System.Collections.Generic;

namespace Yobisoft.IO.Ports
{
    public abstract class ClientPort
        : IClientPort
    {
        public abstract IEnumerable<byte> Receive(int count);
        public abstract void Send(IEnumerable<byte> data);

        public static IClientPort Create(PortType type)
        {
            switch (type)
            {
                case PortType.Serial: return new Implementation.Client.Serial();
                default: throw new NotImplementedException();
            }
        }
    }
}
