using System;

namespace Yobisoft.IO.Modbus
{
    public abstract class Master<T> : IMaster<T>
        where T : IPacket
    {
        public abstract IPacket Query(T request);

        protected internal IPort Port { get; private set; }

        public static IMaster<Packet> Create(ModbusType type, IPort port)
        {
            IMaster<Packet> result;
            switch (type)
            {
                case ModbusType.Rtu: result = new Implementation.Master.Rtu{ Port = port }; break;
                default: throw new NotImplementedException();
            }
            return result;
        }
    }
}
