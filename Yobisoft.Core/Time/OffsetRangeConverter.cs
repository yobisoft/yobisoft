using System;
using System.Collections.Generic;

namespace Yobisoft.Core.Time
{
    /// <summary>
    /// Offset using range converters' base type 
    /// </summary>
    public abstract class OffsetRangeConverter
        : RangeConverter
    {
        /// <summary>
        /// Time offset type
        /// </summary>
        public TimeOffset OffsetType { get; set; } = TimeOffset.Days;

        /// <summary>
        /// Time offset
        /// </summary>
        public int Offset { get; set; } = 1;

        /// <summary>
        /// Converts range start time using predefined converters
        /// </summary>
        /// <param name="now">Current time point</param>
        /// <returns>Start time of a range</returns>
        protected internal override DateTime ConvertStartTime(DateTime now)
        {
            Converter<DateTime, DateTime> converter;
            if (Converters.TryGetValue(OffsetType, out converter))
            {
                return converter(now);
            }
            else
                throw new NotImplementedException("Start Time Converter");
        }
        /// <summary>
        /// Gets or sets converters of a start time 
        /// </summary>
        protected internal Dictionary<TimeOffset, Converter<DateTime, DateTime>> Converters { get; set; }
    }
}
