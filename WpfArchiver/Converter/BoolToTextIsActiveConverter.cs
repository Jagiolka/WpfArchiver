namespace WpfArchiver.Converter;

using System;
using System.Globalization;
using System.Windows.Data;

public class BoolToTextIsActiveConverter : IValueConverter
{
    // Todo: wurde erstmal rausgenommen, kommt später
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool b)
        {
            return b ? "aktiv" : "deaktiviert";
        }

        return string.Empty;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}