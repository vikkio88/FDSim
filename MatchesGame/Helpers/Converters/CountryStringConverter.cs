using System;
using System.Globalization;
using Avalonia.Data.Converters;
using FDSim.Models.Enums;
using FDSim.Models.Enums.Helpers;

namespace MatchesGame.Helpers.Converters;
public class CountryStringConverter : IValueConverter
{
    public static readonly CountryStringConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Nationality source
            && targetType.IsAssignableTo(typeof(string)))
        {
            return NationalityHelper.GetName(source);
        }

        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}