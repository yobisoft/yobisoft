namespace Yobisoft.IO
{
    internal class Helper
    {
        /// <summary>
        /// Gets higher byte of a register
        /// </summary>
        /// <param name="value">Register value</param>
        /// <returns>Higher byte of a register</returns>
        public static byte Hi(ushort value) => (byte)((value >> 8) & 0xFF);

        /// <summary>
        /// Gets lower byte of a register
        /// </summary>
        /// <param name="value">Register value</param>
        /// <returns>Lower byte of a register</returns>
        public static byte Lo(ushort value) => (byte)(value & 0xFF);
    }
}
