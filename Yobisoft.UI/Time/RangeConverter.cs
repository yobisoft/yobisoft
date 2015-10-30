namespace Yobisoft.UI.Time
{
    public class RangeConverter<ConverterType>: System.Windows.Controls.UserControl
        where ConverterType : Core.Time.ITimeRangeConverter, new()
    {
        public Core.Time.ITimeRangeConverter Converter { get; } = new ConverterType();
    }
}
