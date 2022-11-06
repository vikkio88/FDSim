using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace FDSim.Game.Helpers.Converters;
public class DateTimeToString : IValueConverter
{
    public static readonly DateTimeToString Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is DateTime date)
        {
            return date.ToString("ddd, dd MMM yyy");
        }

        return value;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
