using System;
using System.Globalization;
using Avalonia;
using Avalonia.Data.Converters;
using FDSim.Models.Enums.Helpers;

namespace MatchesGame.Views.Common;
public class IsoToFlagConverter : IValueConverter
{
    public static readonly IsoToFlagConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is string nationality && parameter is string isoValue)
        {
            return NationalityHelper.GetIsoCode(nationality) == isoValue;
        }
        return false;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}