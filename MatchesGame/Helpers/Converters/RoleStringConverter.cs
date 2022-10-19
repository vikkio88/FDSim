using System;
using System.Globalization;
using Avalonia.Data.Converters;
using FDSim.Models.Enums;
using FDSim.Models.Enums.Helpers;

namespace MatchesGame.Helpers.Converters;
public class RoleStringConverter : IValueConverter
{
    public static readonly RoleStringConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Role role)
        {
            return RoleHelper.ToString(role);
        }

        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}