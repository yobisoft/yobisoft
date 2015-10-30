using System.Collections.Generic;
using TimeOffset = Yobisoft.Core.Time.TimeOffset;

namespace Yobisoft.UI.Time.RangeConverters
{
    /// <summary>
    /// Interaction logic for Relative.xaml
    /// </summary>
    public partial class Relative : RangeConverter<Yobisoft.Core.Time.RangeConverters.Relative>
    {
        public Relative()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static Core.Time.TimeOffset DefaultValue => Core.Time.TimeOffset.Days;

        public static Dictionary<TimeOffset, string> Items { get; } = new Dictionary<TimeOffset, string>()
        {
            //{ TimeOffset.Milliseconds, "Milliseconds" },
            { TimeOffset.Seconds, "Second(s)" },
            { TimeOffset.Minutes, "Minute(s)" },
            { TimeOffset.Hours, "Hour(s)" },
            { TimeOffset.Days, "Day(s)" },
            { TimeOffset.Months, "Month(s)" },
            { TimeOffset.Years, "Year(s)" },
        };
    }
}
