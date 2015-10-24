using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yobisoft.Core.Time
{
    public interface ITimeRangeConverter
    {
        /// <summary>
        /// Sets StartTime and EndTime of a target range
        /// </summary>
        /// <param name="range">Target range</param>
        void Convert(ITimeRange range);
    }
}
