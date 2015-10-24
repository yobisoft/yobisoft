using System;
using System.Collections.Generic;
using Yobisoft.Core.Extensions;

namespace Yobisoft.Core.Time.RangeConverters
{
    /// <summary>
    /// Time range converter with user defined offset from start position 
    /// </summary>
    public sealed class StartsFrom
        : OffsetRangeConverter
    {
        /// <summary>
        /// Creates 'starts from' time range converter instance
        /// </summary>
        public StartsFrom()
        {
            Converters = new Dictionary<TimeOffset, Converter<DateTime, DateTime>>()
            {
                { TimeOffset.Milliseconds, time => time.ChangeMilliseconds(Offset) },
                { TimeOffset.Seconds, time => time.ChangeMilliseconds(Offset) },
                { TimeOffset.Minutes, time => time.ChangeMilliseconds(Offset) },
                { TimeOffset.Hours, time => time.ChangeMilliseconds(Offset) },
                { TimeOffset.Days, time => time.ChangeDays(Offset) },
                { TimeOffset.Months, time => time.ChangeMonths(Offset) },
                { TimeOffset.Years, time => time.ChangeYears(Offset) },
            };
        }

        /// <summary>
        /// Returns Maxvalue
        /// </summary>
        /// <param name="now">Current time point</param>
        /// <returns>Maximum value of a time range</returns>
        protected internal override DateTime ConvertEndTime(DateTime now)
        {
            return MaxValue;
        }
    }
}
