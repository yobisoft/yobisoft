using System;

namespace Yobisoft.Core.Time.RangeConverters
{
    /// <summary>
    /// Absolute time range converter 
    /// </summary>
    //[NotifyPropertyChanged]
    public sealed class Absolute
        : RangeConverter 
    {
        /// <summary>
        /// Range start time
        /// </summary>
        public DateTime StartTime { get; set; } = DateTime.UtcNow;
        /// <summary>
        /// Range end time
        /// </summary>
        public DateTime EndTime { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Returns user defined start time of a range
        /// </summary>
        /// <param name="now">Current time point</param>
        /// <returns>User defined start point</returns>
        protected internal override DateTime ConvertStartTime(DateTime now)
        {
            return StartTime;
        }

        /// <summary>
        /// Returns user defined end point of a range
        /// </summary>
        /// <param name="now">Current time point</param>
        /// <returns>User defined end point of a range</returns>
        protected internal override DateTime ConvertEndTime(DateTime now)
        {
            return EndTime;
        }
    }
}
