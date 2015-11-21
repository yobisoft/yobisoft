using System.Collections.Generic;

namespace Yobisoft.IO
{
    public interface IPort
    {
        void Send(IEnumerable<byte> data);
        IEnumerable<byte> Receive(int count);
    }
}
