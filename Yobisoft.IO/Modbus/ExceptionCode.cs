namespace Yobisoft.IO.Modbus
{
    /// <summary>
    /// Modbus exception codes
    /// </summary>
    public enum ExceptionCode
        : byte
    {
        None = 0,
        IllegalFunction = 1,
        IllegalDataAddress = 2,
        IllegalDataValue = 3,
        SlaveDeviceFailure = 4,
        Acknowlege = 5,
        SlaveDeviceBusy = 6,
        NegativeAcknowlege = 7,
        MemoryParityError = 8
    }
}
