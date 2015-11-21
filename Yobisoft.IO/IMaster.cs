namespace Yobisoft.IO
{
    public interface IMaster<in T>
        where T: IPacket
    {
        IPacket Query(T request);
    }
}
