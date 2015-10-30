using System.Collections.Generic;
using TimeOffset = Yobisoft.Core.Time.TimeOffset;

namespace Yobisoft.UI.Time.RangeConverters
{
    /// <summary>
    /// Interaction logic for StartsFrom.xaml
    /// </summary>
    public partial class StartsFrom : RangeConverter<Yobisoft.Core.Time.RangeConverters.StartsFrom>
    {
        public StartsFrom()
        {
            InitializeComponent();
            DataContext = this;
        }

        public static Core.Time.TimeOffset DefaultValue => Core.Time.TimeOffset.Days;

        public static Dictionary<TimeOffset, string> Items { get; } = new Dictionary<TimeOffset, string>()
        {
            //{ TimeOffset.Milliseconds, "Milliseconds" },
            { TimeOffset.Seconds, "Second" },
            { TimeOffset.Minutes, "Minute" },
            { TimeOffset.Hours, "Hour" },
            { TimeOffset.Days, "Day" },
            { TimeOffset.Months, "Month" },
            { TimeOffset.Years, "Year" },
        };

        //public static Dictionary<TimeOffset, string> ParentItems { get; } = new Dictionary<TimeOffset, string>()
        //{
        //    //{ TimeOffset.Milliseconds, "Milliseconds" },
        //    { TimeOffset.Seconds, "Minute" },
        //    { TimeOffset.Minutes, "Hour" },
        //    { TimeOffset.Hours, "Day" },
        //    { TimeOffset.Days, "Month" },
        //    { TimeOffset.Months, "Year" },
        //    { TimeOffset.Years, "Age" },
        //};
    }
}
