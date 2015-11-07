namespace Yobisoft.IO.Modbus
{
    public enum Function
    {
        None = 0x00,
        // read functions
        ReadCoilStatus = 0x01,
        ReadInputStatus = 0x02,
        ReadHoldingRegisters = 0x03,
        ReadInputRegisters = 0x04,
        ReadExceptionStatus = 0x07,
        Diagnostic = 0x08,
        FetchCommEventCtr = 0x0B,
        FetchCommEventLog = 0x0C,
        // write functions
        ForceSingleCoil = 0x05,
        PresetSingleRegister = 0x06,
        ForceMultipleCoils = 0x0F,
        PresetMultipleRegs = 0x10,
    }
}
