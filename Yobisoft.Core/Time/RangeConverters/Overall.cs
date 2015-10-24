using System;

namespace Yobisoft.Core.Time.RangeConverters
{
    /// <summary>
    /// Overall time range converter
    /// </summary>
    public sealed class Overall
        : RangeConverter
    {
        /// <summary>
        /// Returns minimum value of a time range
        /// </summary>
        /// <param name="now">Current time point</param>
        /// <returns>Minimum value of a time range</returns>
        protected internal override DateTime ConvertStartTime(DateTime now)
        {
            return MinValue;
        }

        /// <summary>
        /// Returns maximum value of a time range
        /// </summary>
        /// <param name="now">Current time point</param>
        /// <returns>Maximum value of a time range</returns>
        protected internal override DateTime ConvertEndTime(DateTime now)
        {
            return MaxValue;
        }
    }
}
