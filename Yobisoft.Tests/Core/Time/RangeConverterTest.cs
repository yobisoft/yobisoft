using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Yobisoft.Core.Time;
using Yobisoft.Core.Time.RangeConverters;
using System.Collections.Generic;
using System.Reflection;

namespace Yobisoft.Tests.Core.Time
{
    [TestClass]
    public class RangeConverterTest
    {
        class TimeRangeImpl : ITimeRange
        {
            public DateTime EndTime { get; set; }
            public DateTime StartTime { get; set; }
        }

        [TestMethod]
        public void AbsoluteTest()
        {
            var minTime = new DateTime(1);
            var startTime = new DateTime(2);
            var endTime = new DateTime(3);
            var maxTime = new DateTime(4);
            var rc = new Absolute()
            {
                MinValue = minTime,
                StartTime = startTime,
                EndTime = endTime,
                MaxValue = maxTime
            };
            var range = new TimeRangeImpl();
            rc.Convert(range);
            Assert.AreEqual(startTime, range.StartTime);
            Assert.AreEqual(endTime, range.EndTime);
        }

        [TestMethod]
        public void OverallTest()
        {
            var minTime = new DateTime(1);
            var maxTime = new DateTime(4);
            var rc = new Overall()
            {
                MinValue = minTime,
                MaxValue = maxTime
            };
            var range = new TimeRangeImpl();
            rc.Convert(range);
            Assert.AreEqual(minTime, range.StartTime);
            Assert.AreEqual(maxTime, range.EndTime);
        }

        [TestMethod]
        public void RelativeTest()
        {
            var minTime = DateTime.MinValue;
            var maxTime = DateTime.MaxValue;
            var offset = 1;
            var offsetTypes = new Dictionary<TimeOffset, PropertyInfo>()
            {
                {  TimeOffset.Milliseconds, typeof(TimeSpan).GetProperty("Milliseconds")    },
                {  TimeOffset.Seconds,      typeof(TimeSpan).GetProperty("Seconds")         },
                {  TimeOffset.Minutes,      typeof(TimeSpan).GetProperty("Minutes")         },
                {  TimeOffset.Hours,        typeof(TimeSpan).GetProperty("Hours")           },
                {  TimeOffset.Days,         typeof(TimeSpan).GetProperty("Days")            }
                //TimeOffset.Months,
                //TimeOffset.Years
            };
            foreach (var offsetType in offsetTypes)
            {
                var rc = new Relative()
                {
                    MinValue = minTime,
                    Offset = offset,
                    OffsetType = offsetType.Key,
                    MaxValue = maxTime
                };
                var range = new TimeRangeImpl();
                DateTime now = DateTime.UtcNow;
                rc.Convert(range);
                TimeSpan delta = now - range.StartTime;
                Assert.AreEqual(offsetType.Value.GetValue(delta), offset);
            }
        }

        [TestMethod]
        public void StartsFromTest()
        {
            var minTime = DateTime.MinValue;
            var maxTime = DateTime.MaxValue;
            var offset = 1;
            var offsetTypes = new Dictionary<TimeOffset, PropertyInfo>()
            {
                {  TimeOffset.Milliseconds, typeof(DateTime).GetProperty("Millisecond")    },
                {  TimeOffset.Seconds,      typeof(DateTime).GetProperty("Second")         },
                {  TimeOffset.Minutes,      typeof(DateTime).GetProperty("Minute")         },
                {  TimeOffset.Hours,        typeof(DateTime).GetProperty("Hour")           },
                {  TimeOffset.Days,         typeof(DateTime).GetProperty("Day")            },
                {  TimeOffset.Months,       typeof(DateTime).GetProperty("Month")          },
                {  TimeOffset.Years,        typeof(DateTime).GetProperty("Year")           }
            };
            foreach (var offsetType in offsetTypes)
            {
                var rc = new StartsFrom()
                {
                    MinValue = minTime,
                    Offset = offset,
                    OffsetType = offsetType.Key,
                    MaxValue = maxTime
                };
                var range = new TimeRangeImpl();
                DateTime now = DateTime.UtcNow;
                rc.Convert(range);
                Assert.AreEqual(offsetType.Value.GetValue(range.StartTime), offset);
            }
        }
    }
}
