using System;
using Yobisoft.Core.Extensions;

namespace Yobisoft.Core.Time
{
    /// <summary>
    /// Range converters' base type
    /// </summary>
    public abstract class RangeConverter
        : ITimeRangeConverter
    {
        /// <summary>
        /// Minimum value of time range
        /// </summary>
        public DateTime MinValue { get; set; } = DateTime.MinValue;

        /// <summary>
        /// Maximum value of time range
        /// </summary>
        public DateTime MaxValue { get; set; } = DateTime.MaxValue;

        /// <summary>
        /// Sets StartTime and EndTime of a target range
        /// </summary>
        /// <param name="range">Target range</param>
        public void Convert(ITimeRange range)
        {
            DateTime now = DateTime.UtcNow;
            try
            {
                range.StartTime = DateTimeExtension.Max(MinValue, ConvertStartTime(now));
            }
            catch (ArgumentOutOfRangeException)
            {
                range.StartTime = MinValue;
            }
            try
            {
                range.EndTime = DateTimeExtension.Min(MaxValue, ConvertEndTime(now));
            }
            catch (ArgumentOutOfRangeException)
            {
                range.EndTime = MaxValue;
            }
        }

        /// <summary>
        /// Converts now to start time of a range
        /// </summary>
        /// <param name="now">Current time point</param>
        /// <returns>Start time of a range</returns>
        protected internal abstract DateTime ConvertStartTime(DateTime now);

        /// <summary>
        /// Converts now to end time of a range
        /// </summary>
        /// <param name="now">Current time point</param>
        /// <returns>End time of a range</returns>
        protected internal abstract DateTime ConvertEndTime(DateTime now);
    }
}
