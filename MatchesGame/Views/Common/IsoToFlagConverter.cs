using System;
using System.Globalization;
using Avalonia.Data.Converters;

namespace MatchesGame.Views.Common;
public class IsoToFlagConverter : IValueConverter
{
    public static readonly IsoToFlagConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string nationality)
        {
            return $"/Assets/Flags/{nationality}.svg";
        }
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}