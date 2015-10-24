using System;

namespace Yobisoft.Core.Time
{
    /// <summary>
    /// Time range interface
    /// </summary>
    public interface ITimeRange
    {
        /// <summary>
        /// Start time of a range
        /// </summary>
        DateTime StartTime { get; set; }
        /// <summary>
        /// End time of a range
        /// </summary>
        DateTime EndTime { get; set; }
    }
}
