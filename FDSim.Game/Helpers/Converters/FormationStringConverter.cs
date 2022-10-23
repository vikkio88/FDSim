using System;
using System.Globalization;
using Avalonia.Data.Converters;
using FDSim.Models.Enums;
using FDSim.Models.Enums.Helpers;

namespace FDSim.Game.Helpers.Converters;
public class FormationStringConverter : IValueConverter
{
    public static readonly FormationStringConverter Instance = new();

    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (value is Formation source)
        {
            return FormationHelper.GetName(source);
        }

        return string.Empty;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}