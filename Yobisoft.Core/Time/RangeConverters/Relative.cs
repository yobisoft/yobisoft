using System;
using System.Collections.Generic;

namespace Yobisoft.Core.Time.RangeConverters
{
    /// <summary>
    /// Time range converter with user defined offset from current time point
    /// </summary>
    public sealed class Relative
        : OffsetRangeConverter
    {
        /// <summary>
        /// Creates relative time range converter instance
        /// </summary>
        public Relative()
        {
            Converters = new Dictionary<TimeOffset, Converter<DateTime, DateTime>>()
            {
                { TimeOffset.Milliseconds, time => time.AddMilliseconds(-Offset) },
                { TimeOffset.Seconds, time => time.AddSeconds(-Offset) },
                { TimeOffset.Minutes, time => time.AddMinutes(-Offset) },
                { TimeOffset.Hours, time => time.AddHours(-Offset) },
                { TimeOffset.Days, time => time.AddDays(-Offset) },
                { TimeOffset.Months, time => time.AddMonths(-Offset) },
                { TimeOffset.Years, time => time.AddYears(-Offset) },
            };
        }

        /// <summary>
        /// Passes back current time point
        /// </summary>
        /// <param name="now">Current time point</param>
        /// <returns>Current time point</returns>
        protected internal override DateTime ConvertEndTime(DateTime now)
        {
            return now;
        }
    }
}
